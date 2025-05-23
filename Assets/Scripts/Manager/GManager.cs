using FunkyCode.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    [SerializeField]public bool TPFlag = false;
    [Header("���� �� �׷�")]
    public GameObject currentMapGroup; // ���� Ȱ��ȭ�� ���� �巡���ؼ� ���

    [Header("���̵� ��Ʈ�ѷ�")]
    [SerializeField] private FadeInOut m_fadeInOut;
    public FadeInOut IsFadeInOut { get { return m_fadeInOut; } }

    [Header("ī�޶� ����")]
    [SerializeField] private CameraBase m_cameraBase;
    public CameraBase IsCameraBase { get { return m_cameraBase; } }
    [Header("���� ���� ����")]
    [SerializeField] private PotionCraftUI m_potionCraftUI;
    public PotionCraftUI IsPotionCraftUI { get { return m_potionCraftUI; } }
    [Header("���� ����")]
    [SerializeField] private ShopUI m_shopUI;
    public ShopUI IsShopUI { get { return m_shopUI; } }
    [Header("���� ����")]
    public MapBGMController mapBGMController;
    [SerializeField] private SoundManager m_soundManager;
    public SoundManager IsSoundManager { get { return m_soundManager; } }
    /// <summary>
    /// ���� ��Ʈ�ѷ�
    /// </summary>
    public UserController IsUserController = null;
    /// <summary>
    /// ���� Ʈ������
    /// </summary>
    public Transform IsUserTrans
    {
        get { return m_userObj != null ? m_userObj.transform : null; }
    }

    /// <summary>
    /// ���� ���� ������Ʈ
    /// </summary>
    GameObject m_userObj = null;
    /// <summary>
    /// �κ��丮
    /// </summary>
    [SerializeField] InventoryUI m_inventoryUI = null;
    public InventoryUI IsInventoryUI { get { return m_inventoryUI; } }
    /// <summary>
    /// �κ� �Ŵ���
    /// </summary>
    public InventoryManager IsinvenManager { get { return m_invenManager; } }
    /// <summary>
    /// �κ��丮 �Ŵ���
    /// </summary>
    [SerializeField] InventoryManager m_invenManager = null;
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
    /// ��ȭ �Ŵ���
    /// </summary>
    [SerializeField] DialogueManager m_dialogueManager = null;
    public DialogueManager IsDialogueManager { get { return m_dialogueManager; } }

    /// <summary>
    /// ����Ʈ �Ŵ���
    /// </summary>
    [SerializeField] QuestManager m_questManager = null;
    public QuestManager IsQuestManager { get { return m_questManager; } }

    /// <summary>
    /// ���� UI
    /// </summary>
    [SerializeField] DialogueUI m_dialogueUI = null;
    public DialogueUI IsDialougeUI { get { return m_dialogueUI; } }


    /// <summary>
    /// ���� �÷���
    /// �� ��ȯ�� false��
    /// </summary>
    public bool IsSettingFlag { get; set; } = false;

    public bool m_uIPrev = false;

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
    public void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "MainGame")
        {
            GameObject m_character = GameObject.Find("Character");

            if (m_character != null)
            {
                Setting(m_character); // ���⼭ IsUserTrans ������
            }
            else
            {
            }
        }
        else
        {
        }

        InitFirstMapBounds();

        if (currentMapGroup != null && mapBGMController != null)
        {
            mapBGMController.PlayBGMForMap(currentMapGroup);
        }
    }

    void Update()
    {
        if (IsUIManager == null || IsUserController == null) return;

        bool isUI = IsUIManager.UIOpenFlag;

        if (isUI != m_uIPrev)
        {
            IsUserController.SetMoveFlag(!isUI);
            m_uIPrev = isUI;
        }
    }    /// <summary>
         /// ����
         /// </summary>
         /// <param name="argUserObj">���� ������Ʈ</param>
    public void Setting(GameObject argUserObj)
    {
        m_userObj = argUserObj;
        IsSettingFlag = true;
    }


    public void SetInventoryUI(InventoryUI ui)
    {
        m_inventoryUI = ui; 
    }
    public void SetTPFlag(bool isOn)
    {
        TPFlag = isOn;
    }
    public void StartTPAfterTeleport()
    {
        if (m_fadeInOut != null)
        {
            StartCoroutine(TPAfterTeleportCoroutine());
        }
    }

    private IEnumerator TPAfterTeleportCoroutine()
    {
        yield return StartCoroutine(m_fadeInOut.FadeIn());
        SetTPFlag(false);
    }
    public void InitFirstMapBounds()
    {
        if (currentMapGroup == null || IsCameraBase == null)
        {
            return;
        }

        var collider = currentMapGroup.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            return;
        }

        Bounds bounds = collider.bounds;
        IsCameraBase.SetCameraBounds(bounds.min, bounds.max);

    }
    public void AutoReferenceSceneObjects()
    {
        // ī�޶�
        m_cameraBase = FindObjectOfType<CameraBase>();
        Debug.Log($"[AutoRef] CameraBase: {(m_cameraBase != null ? "������" : "NULL")}");

        // ��Ʈ�ѷ�
        IsUserController = FindObjectOfType<UserController>();
        Debug.Log($"[AutoRef] UserController: {(IsUserController != null ? "������" : "NULL")}");

        // ��
        currentMapGroup = GameObject.Find("MapM0_CityHall");
        Debug.Log($"[AutoRef] currentMapGroup: {(currentMapGroup != null ? "������" : "NULL")}");

        // ���̵� �ξƿ�
        m_fadeInOut = FindObjectOfType<FadeInOut>();
        Debug.Log($"[AutoRef] FadeInOut: {(m_fadeInOut != null ? "������" : "NULL")}");

        // UI
        m_potionCraftUI = FindObjectOfType<PotionCraftUI>();
        Debug.Log($"[AutoRef] PotionCraftUI: {(m_potionCraftUI != null ? "������" : "NULL")}");

        m_shopUI = FindObjectOfType<ShopUI>();
        Debug.Log($"[AutoRef] ShopUI: {(m_shopUI != null ? "������" : "NULL")}");

        m_craftUI = FindObjectOfType<CraftUI>();
        Debug.Log($"[AutoRef] CraftUI: {(m_craftUI != null ? "������" : "NULL")}");

        m_dialogueUI = FindObjectOfType<DialogueUI>();
        Debug.Log($"[AutoRef] DialogueUI: {(m_dialogueUI != null ? "������" : "NULL")}");

        m_inventoryUI = GameObject.Find("Inventory")?.GetComponent<InventoryUI>();
        Debug.Log($"[AutoRef] InventoryUI: {(m_inventoryUI != null ? "������" : "NULL")}");

        // �Ŵ���
        m_invenManager = FindObjectOfType<InventoryManager>();
        Debug.Log($"[AutoRef] InventoryManager: {(m_invenManager != null ? "������" : "NULL")}");

        m_UIManager = FindObjectOfType<UIManager>();
        Debug.Log($"[AutoRef] UIManager: {(m_UIManager != null ? "������" : "NULL")}");

        m_exchangeManager = FindObjectOfType<ExchangeManager>();
        Debug.Log($"[AutoRef] ExchangeManager: {(m_exchangeManager != null ? "������" : "NULL")}");

        m_dialogueManager = FindObjectOfType<DialogueManager>();
        Debug.Log($"[AutoRef] DialogueManager: {(m_dialogueManager != null ? "������" : "NULL")}");

        // UIManager ���� �ʵ嵵 �ڵ� ����
        if (m_UIManager != null)
        {
            m_UIManager.CraftUI = GameObject.Find("CraftUI");
            m_UIManager.PotionCraftUI = GameObject.Find("PotionCraftUI");
            m_UIManager.ShopUI = GameObject.Find("ShopUI");
            m_UIManager.DialogueUI = GameObject.Find("DialogueUI");

            Debug.Log($"[AutoRef] UIManager ���� CraftUI: {(m_UIManager.CraftUI != null ? "������" : "NULL")}");
            Debug.Log($"[AutoRef] UIManager ���� PotionCraftUI: {(m_UIManager.PotionCraftUI != null ? "������" : "NULL")}");
            Debug.Log($"[AutoRef] UIManager ���� ShopUI: {(m_UIManager.ShopUI != null ? "������" : "NULL")}");
            Debug.Log($"[AutoRef] UIManager ���� DialogueUI: {(m_UIManager.DialogueUI != null ? "������" : "NULL")}");
        }
    }

}
