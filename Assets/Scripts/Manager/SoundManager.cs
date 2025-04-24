using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("BGM �ҽ�")]
    [SerializeField] private AudioSource bgmSource;

    [Header("SFX �ҽ� (�÷��̾� + �ý��� ����)")]
    [SerializeField] private AudioSource sfxSource;

    [Header("�÷��̾� ���� ���")]
    public List<NamedSound> playerSounds;

    [Header("�ý��� ���� ���")]
    public List<NamedSound> systemSounds;

    private Dictionary<string, AudioClip> playerSoundDict;
    private Dictionary<string, AudioClip> systemSoundDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerSoundDict = playerSounds.ToDictionary(s => s.name, s => s.clip);
            systemSoundDict = systemSounds.ToDictionary(s => s.name, s => s.clip);
        }
    }

    public void PlayPlayerSound(string name)
    {
        if (playerSoundDict.TryGetValue(name, out var clip))
        {
            sfxSource.PlayOneShot(clip);
            Debug.Log($"[SoundManager] �÷��̾� ���� ���: {name}");
        }
        else
        {
            Debug.LogWarning($"[SoundManager] �÷��̾� ���� '{name}'�� �������� ����");
        }
    }


    public void PlaySystemSound(string name)
    {
        if (systemSoundDict.TryGetValue(name, out var clip))
        {
            sfxSource.PlayOneShot(clip);
            Debug.Log($"[SoundManager] �ý��� ���� ���: {name}");
        }
        else
        {
            Debug.LogWarning($"[SoundManager] �ý��� ���� '{name}'�� �������� ����");
        }
    }


    public void PlayBGM(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("[SoundManager] PlayBGM: clip�� null�Դϴ�.");
            return;
        }

        if (bgmSource == null)
        {
            Debug.LogError("[SoundManager] bgmSource�� ����Ǿ� ���� �ʽ��ϴ�!");
            return;
        }

        Debug.Log($"[SoundManager] ��� �غ�: {clip.name}");
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();

        Debug.Log($"[SoundManager]  ���ο� BGM ��� ����: {clip.name}");
    }
}

[System.Serializable]
public class NamedSound
{
    public string name;
    public AudioClip clip;
}
