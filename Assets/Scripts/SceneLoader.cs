using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    private static Action onAfterSceneLoad;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[SceneLoader] �� �ε��: {scene.name}");

        GManager.Instance?.AutoReferenceSceneObjects();

        if (scene.name == "MainGame")
        {
            GManager.Instance.InitFirstMapBounds();

            GameObject character = GameObject.Find("Character");
            if (character != null)
            {
                GManager.Instance?.Setting(character);
                Debug.Log("[SceneLoader] GManager.Setting ȣ�� �Ϸ�");
            }
            else
            {
                Debug.LogError("[SceneLoader] MainGame�� Character�� ����");
            }
        }

        onAfterSceneLoad?.Invoke(); // �� �� ���� ����
        onAfterSceneLoad = null;    // ���� �� ����
    }

    public static void LoadScene(string sceneName, Action afterLoad = null)
    {
        onAfterSceneLoad = afterLoad; // �� �ε� �� ����� �ݹ� ����
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        onAfterSceneLoad = null; // �ݹ� ���� �ε�
        SceneManager.LoadScene(sceneName);
    }
}
