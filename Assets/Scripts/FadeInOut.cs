using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (fadeImage != null)
        {
            // ���� ������ �� fadeImage�� �����ϰ� �ʱ�ȭ
            Color color = fadeImage.color;
            color.a = 0f; // ���ĸ� 0����
            fadeImage.color = color;
        }
        else
        {
            Debug.LogError("[FadeInOut] fadeImage�� ����Ǿ� ���� �ʽ��ϴ�!");
        }
    }

    public IEnumerator FadeOut()
    {
        if (fadeImage == null) yield break;

        Color color = fadeImage.color;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    public IEnumerator FadeIn()
    {
        if (fadeImage == null) yield break;

        Color color = fadeImage.color;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }
}
