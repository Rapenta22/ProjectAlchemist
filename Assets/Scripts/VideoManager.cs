using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [Header("���� ��¿� ĵ����")]
    [SerializeField] private GameObject m_videoCanvas;
    [Header("���� �÷��̾�")]
    [SerializeField] private VideoPlayer m_videoPlayer;

    private bool m_isVideoPlaying = false;

    void Awake()
    {
        m_videoPlayer.loopPointReached += OnVideoEnd;
        m_videoCanvas.SetActive(false);
    }

    public IEnumerator PlayVideoRoutine(VideoClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("[VideoManager] ����� ������ �����ϴ�!");
            yield break;
        }

        // ���� ���� ��: FadeOut-In
        yield return StartCoroutine(GManager.Instance.IsFadeInOut.FadeOut());

        m_videoPlayer.clip = clip;
        m_videoCanvas.SetActive(true);
        m_videoPlayer.Play();
        m_isVideoPlaying = true;

        yield return StartCoroutine(GManager.Instance.IsFadeInOut.FadeIn());

        // ���� ������ ���
        bool videoEnded = false;
        m_videoPlayer.loopPointReached += (vp) => videoEnded = true;
        while (!videoEnded)
            yield return null;

        // ���� ������ FadeOut (���� �ݱ�)
        yield return StartCoroutine(GManager.Instance.IsFadeInOut.FadeOut());
        m_videoCanvas.SetActive(false);
        m_isVideoPlaying = false;
        yield return StartCoroutine(GManager.Instance.IsFadeInOut.FadeIn());

    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // �ڷ�ƾ���� ó�� ��
    }
}
