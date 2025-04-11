using FunkyCode.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{

    [SerializeField] private PotionCraftUI m_potionCraftUI;
    public PotionCraftUI IsPotionCraftUI { get { return m_potionCraftUI; } }


    /// <summary>
    /// ����
    /// </summary>
    [SerializeField] private UserController m_user = null;
    /// <summary>
    /// ���� ���� ������Ʈ
    /// </summary>
    GameObject m_userObj = null;
    /// <summary>
    /// �κ��丮
    /// </summary>
    public InventoryUI InventoryUI { get; set; }
    /// <summary>
    /// �κ� �Ŵ���
    /// </summary>
    public InventoryManager IsinvenManager { get { return m_invenManager; } }
    /// <summary>
    /// �κ��丮 �Ŵ���
    /// </summary>
    [SerializeField] InventoryManager m_invenManager = null;
    public InventoryManager IsInvenManager { get; set; }
    /// <summary>
    /// UI�Ŵ���
    /// </summary>
    [SerializeField] UIManager m_UIManager = null;
    public UIManager IsUIManager { get { return m_UIManager; } }

    /// <summary>
    /// ���� UI
    /// </summary>
    [SerializeField] CraftUI m_craftUI = null;

    public CraftUI IsCraftUI { get { return m_craftUI; } }
    /// <summary>
    /// ��ȯ �Ŵ���
    /// </summary>
    [SerializeField] ExchangeManager m_exchangeManager = null;
    public ExchangeManager IsExchangeManager { get {return  m_exchangeManager;} }




    /// <summary>
    /// ���� �÷���
    /// �� ��ȯ�� false��
    /// </summary>
    public bool IsSettingFlag { get; set; } = false;


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
    /// ���� ��Ʈ�ѷ�
    /// </summary>
    public UserController IsUserController { get; private set; } = null;




    /// <summary>
    /// ���� Ʈ������
    /// </summary>
    public Transform IsUserTrans
    {
        get { return m_userObj != null ? m_userObj.transform : null; }
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

    public void SetInventoryUI(InventoryUI ui)
    {
        InventoryUI = ui;
    }

}
