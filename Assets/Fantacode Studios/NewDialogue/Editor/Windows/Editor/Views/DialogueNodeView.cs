using UnityEngine;
using UnityEditor;

public class DialogueNodeView
{
    public DialogueNode m_node;
    public Rect m_rect;

    private bool m_isDragging = false;
    private Vector2 m_dragOffset;

    public bool m_isSelected = false;

    public DialogueNodeView(DialogueNode node, Vector2 position)
    {
        this.m_node = node;
        this.m_rect = new Rect(node.m_position.x, node.m_position.y, 200, 100);
    }

    public void Draw()
    {
        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.alignment = TextAnchor.UpperLeft;

        GUI.color = m_isSelected ? Color.cyan : Color.white;
        GUI.Box(m_rect, "", style);
        GUI.color = Color.white;

        // �̸� ���� �ʵ�
        if (string.IsNullOrEmpty(m_node.m_nodeName))
            m_node.m_nodeName = m_node.name; // �ʱⰪ�� ����

        m_node.m_nodeName = EditorGUI.TextField(
            new Rect(m_rect.x + 10, m_rect.y + 5, m_rect.width - 20, 18),
            m_node.m_nodeName
        );

        // Ÿ�� ǥ�� (������)
        GUI.Label(new Rect(m_rect.x + 10, m_rect.y + 26, m_rect.width - 20, 20), "[Dialogue]");

        // �̸� �ؽ�Ʈ �ʵ�
        m_node.name = EditorGUI.TextField(new Rect(m_rect.x + 10, m_rect.y + 5, m_rect.width - 20, 18), m_node.name);

        // Ÿ�� + ��ȭ�� ǥ��
        string speakerLine = $"[Dialogue] {(string.IsNullOrEmpty(m_node.m_speakerName) ? "???" : m_node.m_speakerName)}";
        GUI.Label(new Rect(m_rect.x + 10, m_rect.y + 26, m_rect.width - 20, 20), speakerLine);

        // ������ ��� ���� �̸�����
        string preview = m_node.m_dialogueText;
        if (!string.IsNullOrEmpty(preview) && preview.Length > 30)
            preview = preview.Substring(0, 30) + "...";

        GUI.Label(new Rect(m_rect.x + 10, m_rect.y + 46, m_rect.width - 20, 40), preview, EditorStyles.wordWrappedLabel);
    }

    public void ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0 && m_rect.Contains(e.mousePosition))
                {
                    m_isDragging = true;
                    m_dragOffset = e.mousePosition - m_rect.position;
                    Selection.activeObject = m_node; //  Inspector�� ���� �ݿ�
                    e.Use();
                }
                else if (e.button == 1 && m_rect.Contains(e.mousePosition))
                {
                    ShowContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && m_isDragging)
                {
                    m_rect.position = e.mousePosition - m_dragOffset;
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                if (e.button == 0)
                {
                    m_isDragging = false;
        m_node.m_position = m_rect.position;
                }
                break;
        }
    }

    public void ShowContextMenu()
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Delete Node"), false, () =>
        {
            DialogueEditorWindow.RequestNodeDelete(this);
        });
        menu.ShowAsContext();
    }

    public void Drag(Vector2 delta)
    {
        m_rect.position += delta;
    }

    public Vector2 GetOutputPosition()
    {
        return new Vector2(m_rect.xMax, m_rect.center.y);
    }

    public Vector2 GetInputPosition()
    {
        return new Vector2(m_rect.xMin, m_rect.center.y);
    }

    public bool ContainsPoint(Vector2 point)
    {
        return m_rect.Contains(point);
    }
}
