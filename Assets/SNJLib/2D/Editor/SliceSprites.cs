using UnityEngine;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using System.Collections.Generic;
using System.IO;

public class SliceSprites : EditorWindow
{
    /// <summary>
    /// ���õ� �ؽ��ĸ� �����ϴ� ����Ʈ
    /// </summary>
    private List<Texture2D> m_selectedTexturesList = new List<Texture2D>();

    /// �����̽��� ���� ��
    /// </summary>
    private int m_columns = 1;

    /// <summary>
    /// �����̽��� ���� �� 
    /// </summary>
    private int m_rows = 1;

    /// <summary>
    /// �߽��� ����Ʈ
    /// </summary>
    private Vector2 m_pivot = new Vector2(0.5f, 0.5f);

    /// <summary>
    /// ��ũ�� ��ġ �����
    /// </summary>
    private Vector2 m_scrollPosition;

    /// <summary>
    /// ������ ������ ǥ�� �Լ�
    /// </summary>
    [MenuItem("SNJ Tools/Slice Sprites")]
    public static void ShowWindow()
    {
        SliceSprites window = (SliceSprites)GetWindow(typeof(SliceSprites), false, "Slice Sprites");
        window.minSize = new Vector2(300, 400);
    }

    /// <summary>
    /// ������ ������ GUI ���� �Լ�
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label("Sprite Slicer", EditorStyles.boldLabel);

        GUILayout.Label("Drag Textures Here", EditorStyles.boldLabel);
        Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drop Textures Here");

        // �巡�� �� ��� ó��
        HandleDragAndDrop(dropArea);

        GUILayout.Label("Selected Textures:", EditorStyles.boldLabel);
        m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition, GUILayout.Height(position.height - 200));

        // ���õ� �ؽ�ó���� ǥ��
        foreach (var texture in m_selectedTexturesList)
        {
            string _path = AssetDatabase.GetAssetPath(texture);
            string _fileName = Path.GetFileName(_path);
            GUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent(texture), GUILayout.Width(50), GUILayout.Height(50));
            GUILayout.Label($"{texture.name} ({_fileName})");
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

        GUILayout.FlexibleSpace();

        // �����̽��� ���� ���� ��, �߽��� ����Ʈ�� �Է� ����
        GUILayout.BeginVertical();
        m_columns = EditorGUILayout.IntField("Columns", m_columns);
        m_rows = EditorGUILayout.IntField("Rows", m_rows);
        m_pivot = EditorGUILayout.Vector2Field("Pivot", m_pivot);

        // Apply ��ư�� Ŭ���ϸ� �����̽� �Լ��� ȣ��
        if (GUILayout.Button("Apply"))
        {
            SliceSelectedSprites();
        }
        GUILayout.EndVertical();
    }

    /// <summary>
    /// �巡�� �� ��� ó�� �Լ�
    /// </summary>
    /// <param name="argDropArea">��� �����</param>
    private void HandleDragAndDrop(Rect argDropArea)
    {
        Event evt = Event.current;
        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!argDropArea.Contains(evt.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    m_selectedTexturesList.Clear();
                    foreach (var draggedObject in DragAndDrop.objectReferences)
                    {
                        Texture2D texture = draggedObject as Texture2D;
                        if (texture != null)
                        {
                            m_selectedTexturesList.Add(texture);
                        }
                    }
                }
                Event.current.Use();
                break;
        }
    }

    /// <summary>
    /// ��������Ʈ �����̽� �Լ�
    /// </summary>
    private void SliceSelectedSprites()
    {
        foreach (var _texture in m_selectedTexturesList)
        {
            string _path = AssetDatabase.GetAssetPath(_texture);
            TextureImporter _importer = AssetImporter.GetAtPath(_path) as TextureImporter;

            if (_importer != null)
            {
                // �ؽ�ó�� �Ӽ��� ����
                _importer.textureType = TextureImporterType.Sprite;
                _importer.spriteImportMode = SpriteImportMode.Multiple;
                _importer.filterMode = FilterMode.Point;
                _importer.textureCompression = TextureImporterCompression.Uncompressed;

                _importer.isReadable = true;
                AssetDatabase.ImportAsset(_path, ImportAssetOptions.ForceUpdate);

                int _spriteWidth = _texture.width / m_columns;
                int _spriteHeight = _texture.height / m_rows;

                List<SpriteRect> _spriteRects = new List<SpriteRect>();

                // �ؽ�ó�� �����̽��Ͽ� SpriteRect ����Ʈ�� �߰�
                for (int _y = 0; _y < m_rows; _y++)
                {
                    for (int _x = 0; _x < m_columns; _x++)
                    {
                        SpriteRect _spriteRect = new SpriteRect
                        {
                            rect = new Rect(_x * _spriteWidth, _y * _spriteHeight, _spriteWidth, _spriteHeight),
                            name = _texture.name + "_" + (_y * m_columns + _x),
                            pivot = m_pivot
                        };
                        _spriteRects.Add(_spriteRect);
                    }
                }

                // �����̽��� ��������Ʈ �����͸� ����
                ApplySpriteSlices(_importer, _spriteRects);
                EditorUtility.SetDirty(_importer);
                _importer.SaveAndReimport();
            }
            else
            {
                Debug.LogError("Selected texture importer is not valid: " + _texture.name);
            }
        }

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// �����̽��� ��������Ʈ �����͸� �����ϴ� �Լ�
    /// </summary>
    /// <param name="argImporter">�ؽ��� ������</param>
    /// <param name="argSpriteRects">��������Ʈ Rect ����Ʈ</param>
    private void ApplySpriteSlices(TextureImporter argImporter, List<SpriteRect> argSpriteRects)
    {
        SerializedObject _serializedObject = new SerializedObject(argImporter);
        SerializedProperty _spriteSheetSP = _serializedObject.FindProperty("m_SpriteSheet.m_Sprites");

        _spriteSheetSP.arraySize = argSpriteRects.Count;
        for (int i = 0; i < argSpriteRects.Count; i++)
        {
            SerializedProperty _spriteRectSP = _spriteSheetSP.GetArrayElementAtIndex(i);

            _spriteRectSP.FindPropertyRelative("m_Rect").rectValue = argSpriteRects[i].rect;
            _spriteRectSP.FindPropertyRelative("m_Name").stringValue = argSpriteRects[i].name;
            _spriteRectSP.FindPropertyRelative("m_Pivot").vector2Value = argSpriteRects[i].pivot;
            _spriteRectSP.FindPropertyRelative("m_Alignment").intValue = (int)SpriteAlignment.Custom;
        }

        _serializedObject.ApplyModifiedProperties();
    }
}
