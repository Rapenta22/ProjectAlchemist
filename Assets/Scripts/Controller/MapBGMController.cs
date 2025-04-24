using UnityEngine;
using System.Collections.Generic;

public class MapBGMController : MonoBehaviour
{
    [System.Serializable]
    public class MapEntry
    {
        public GameObject mapObject;
        public AudioClip overrideBGM; // null�̸� �׷� BGM ���
    }

    [System.Serializable]
    public class MapGroupEntry
    {
        public MapGroupType groupType;
        public AudioClip groupBGM;
        public List<MapEntry> maps; // �ش� �׷쿡 ���� �ʵ�
    }

    [Header("�׷캰 �� & BGM ����")]
    public List<MapGroupEntry> mapGroups;

    private AudioClip currentClip;
    void Start()
    {
        if (mapGroups != null && mapGroups.Count > 0)
        {
            var group = mapGroups[0];
            Debug.Log($"[Test] groupBGM before: {group.groupBGM}");

            if (group.groupBGM == null)
            {
                group.groupBGM = Resources.Load<AudioClip>("Sounds/Town_bgm"); // ��ο� �°� ����
                Debug.Log($"[Test] groupBGM loaded manually: {group.groupBGM}");
            }
        }
    }
    public void PlayBGMForMap(GameObject mapObject)
    {
        if (mapObject == null)
        {
            Debug.LogWarning("[MapBGMController] mapObject is null. BGM ��� ����");
            return;
        }

        Debug.Log($"[MapBGMController] ���� - ���: {mapObject.name}");

        foreach (var group in mapGroups)
        {
            foreach (var entry in group.maps)
            {
                if (entry.mapObject == mapObject)
                {
                    Debug.Log($"[MapBGMController] ��Ī ����: {entry.mapObject.name}");

                    AudioClip clipToPlay = group.groupBGM;

                    if (clipToPlay == null || string.IsNullOrWhiteSpace(clipToPlay.name))
                    {
                        Debug.LogWarning($"[MapBGMController] {mapObject.name}�� �׷� BGM�� null�Դϴ�.");
                        return;
                    }

                    if (clipToPlay == currentClip)
                    {
                        Debug.Log($"[MapBGMController] ������ BGM '{clipToPlay.name}'�� �̹� ��� ���Դϴ�.");
                        return;
                    }

                    currentClip = clipToPlay;
                    SoundManager.Instance.PlayBGM(clipToPlay);

                    Debug.Log($"[MapBGMController]  BGM ��� ����: {clipToPlay.name} (Group: {group.groupType})");
                    return;
                }
            }
        }

        Debug.LogWarning($"[MapBGMController] '{mapObject.name}'�� � �׷쿡�� ��ϵ��� �ʾҽ��ϴ�.");
    }

}