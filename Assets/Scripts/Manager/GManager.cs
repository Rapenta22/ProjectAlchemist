using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    /// <summary>
    /// ���� ���� ������Ʈ
    /// </summary>
    GameObject m_userObj = null;

    /// <summary>
    /// ���� �÷���
    /// �� ��ȯ�� false��
    /// </summary>
    public bool IsSettingFlag { get; set; } = false;

    public InventoryManager IsInvenManager { get; private set; } = null;

    /// <summary>
    /// �̱��� �ν��Ͻ�
    /// </summary>
    public static GManager Instance { get; private set; } = null;

    private void Awake()
    {

        if (GManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="argUserObj">���� ������Ʈ</param>
    public void Setting(GameObject argUserObj)
    {
        m_userObj = argUserObj;
        IsSettingFlag = true;
    }

    /// <summary>
    /// ���� Ʈ������ ��ȯ (null üũ �߰�)
    /// </summary>
    public Transform IsUserTrans
    {
        get
        {
            if (m_userObj == null)
            {
                return null;
            }
            return m_userObj.transform;
        }
    }

    private void Start()
    {
        //  Start()���� �ڵ����� Setting() ȣ��
        GameObject m_character = GameObject.Find("Character");
        if (m_character != null)
        {
            Setting(m_character);
        }
    }


}
