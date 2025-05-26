using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    private static string targetScene;
    private static Action onAfterSceneLoad;
    private static bool playIntro;

    void Awake()
    {
        Debug.Log("[SceneLoader] Awake() ���� - �ߺ� ���� ���� �� �ʱ�ȭ");
        // �ߺ� ���� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[SceneLoader] �� �ε��: {scene.name}");

        if (GManager.Instance != null)
        {
            Debug.Log("[SceneLoader] GManager.Instance.AutoReferenceSceneObjects() ȣ��");
            GManager.Instance.AutoReferenceSceneObjects();
        }
        else
        {
            Debug.LogWarning("[SceneLoader] GManager.Instance�� null�Դϴ�!");
        }

        StartCoroutine(DelayedStart(scene));
    }

    private IEnumerator DelayedStart(Scene scene)
    {
        Debug.Log("[SceneLoader] DelayedStart() - �� ������ ���");
        yield return null;  // �� ������Ʈ�� ��� �ε�ǰ� Ȱ��ȭ�� ������ �� ������ ���

        if (scene.name == "LoadingScene")
        {
            Debug.Log("[SceneLoader] LoadingScene���� StartLoading ȣ��");
            GManager.Instance.IsLoadingManager.StartLoading(targetScene, playIntro);
        }
        else if (scene.name == "MainGame")
        {
            Debug.Log("[SceneLoader] MainGame�� ����");

            // �� ã��
            GameObject map = GameObject.Find("MapM0_CityHall"); // ���� �°� �̸� ����
            if (map != null)
            {
                Debug.Log("[SceneLoader] �� ������Ʈ ã��");
                BoxCollider2D mapCollider = map.GetComponent<BoxCollider2D>();
                if (mapCollider != null && GManager.Instance.IsCameraBase != null)
                {
                    Debug.Log("[SceneLoader] �� �ݶ��̴��� ī�޶��̽� ã��");
                    var bounds = mapCollider.bounds;
                    GManager.Instance.IsCameraBase.SetCameraBounds(bounds.min, bounds.max);
                    Debug.Log($"[SceneLoader] Camera bounds set: min={bounds.min}, max={bounds.max}");
                }
                else
                {
                    Debug.LogWarning("[SceneLoader] �� �ݶ��̴� �Ǵ� ī�޶� ���̽��� ã�� �� �����ϴ�.");
                }
            }
            else
            {
                Debug.LogWarning("[SceneLoader] �� ������Ʈ�� ã�� �� �����ϴ�.");
            }

            // ĳ���� ã�� �� ����
            var character = GameObject.Find("Character");
            if (character != null)
            {
                Debug.Log("[SceneLoader] ĳ���� ã��");
                GManager.Instance.Setting(character);
                Debug.Log("[SceneLoader] GManager.Setting ȣ�� �Ϸ�");
            }
            else
            {
                Debug.LogWarning("[SceneLoader] ĳ���� ������Ʈ�� ã�� �� �����ϴ�.");
            }

            // onAfterSceneLoad �׼��� ������ ȣ��
            if (onAfterSceneLoad != null)
            {
                Debug.Log("[SceneLoader] onAfterSceneLoad �ݹ� ȣ��");
            }
            onAfterSceneLoad?.Invoke();
            onAfterSceneLoad = null;
        }
        else
        {
            Debug.Log($"[SceneLoader] {scene.name} ������ Ư���� ó���� �����ϴ�.");
            onAfterSceneLoad?.Invoke();
            onAfterSceneLoad = null;
        }
    }

    public static void LoadScene(string sceneName, bool isPlayIntro, Action afterLoad = null)
    {
        Debug.Log($"[SceneLoader] LoadScene ȣ��! targetScene={sceneName}, playIntro={isPlayIntro}");
        targetScene = sceneName;
        playIntro = isPlayIntro;
        onAfterSceneLoad = afterLoad;

        Debug.Log("[SceneLoader] FadeInOut�� ���� �ε������� ��ȯ ����");
        GManager.Instance.StartCoroutine(GManager.Instance.IsFadeInOut.LoadSceneWithFade("LoadingScene"));
    }
}
