using UnityEngine;

public class MapInfo : MonoBehaviour
{
    [Header("�� ���� ���� �׷�")]
    public MapGroupType groupType;

    [Header("�� �� ���� BGM (����θ� �׷� BGM ���)")]
    public AudioClip overrideBGM;

    [Header("�� ���� ���� �̸� (����, �ε�, ����Ʈ�� ��)")]
    public string mapID;

    [Header("�� �ʿ��� ǥ���� �̸� (UI�� ��)")]
    public string displayName;

    // ���� ����Ʈ/����/���� �̺�Ʈ�� ���� Ȯ�� �ʵ嵵 ���⿡ �߰� ����
}
