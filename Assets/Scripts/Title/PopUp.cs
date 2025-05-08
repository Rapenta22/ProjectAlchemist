using UnityEngine;
using UnityEngine.UI;
using System;

public class PopUp : MonoBehaviour
{
    [SerializeField] GameObject[] m_options;   // [0] Yes, [1] No
    [SerializeField] Sprite[] m_sprites;       // [0] �⺻, [1] ����
    [SerializeField] GameObject m_popup;

    private int m_selectedIndex = 0;
    private Action<bool> onResultCallback;

    public void Open(Action<bool> resultCallback)
    {
        onResultCallback = resultCallback;
        m_selectedIndex = 0;
        UpdateHighlight();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        m_popup.SetActive(false);
        FindObjectOfType<TitleSc>().m_boxOpenFlag = false;
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        HandleDirectionInput();
        HandleConfirmInput();
    }

    /// <summary>
    /// ����Ű �Է� ó��: �¿� ȭ��ǥ�� ���� �ε����� ��ȯ
    /// </summary>
    private void HandleDirectionInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_selectedIndex = 1 - m_selectedIndex;
            UpdateHighlight();
        }
    }

    /// <summary>
    /// Space Ű �Է� �� ��� ���� �� �˾� �ݱ�
    /// </summary>
    private void HandleConfirmInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onResultCallback?.Invoke(m_selectedIndex == 0); // true: Yes, false: No
            Close();
        }
    }

    /// <summary>
    /// ���� �ε����� ���� ���̶���Ʈ ��������Ʈ ����
    /// </summary>
    private void UpdateHighlight()
    {
        for (int i = 0; i < m_options.Length; i++)
        {
            var img = m_options[i].GetComponent<Image>();
            if (img != null)
                img.sprite = (i == m_selectedIndex) ? m_sprites[1] : m_sprites[0];
        }
    }
}
