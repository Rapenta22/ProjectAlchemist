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
    /// ���� �÷���
    /// �� ��ȯ�� false��
    /// </summary>
    public bool IsSettingFlag { get; set; } = false;

    public bool m_uiPrev = false;

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
                Debug.Log("[GManager] Setting() ȣ���: " + m_character.name);
            }
            else
            {
                Debug.LogError("[GManager] MainGame �������� Character ������Ʈ�� ã�� �� ����!");
            }
        }
        else
        {
            Debug.Log($"[GManager] ���� ��({currentScene})�� ĳ���� ������ �ʿ� ���� ���Դϴ�.");
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

        if (isUI != m_uiPrev)
        {
            IsUserController.SetMoveFlag(!isUI);
            Debug.Log($"[GManager] UI ���� ����: �̵� {(isUI ? "����" : "���")}");
            m_uiPrev = isUI;
        }
    }    /// <summary>
         /// ����
         /// </summary>
         /// <param name="argUserObj">���� ������Ʈ</param>
    public void Setting(GameObject argUserObj)
    {
        m_userObj = argUserObj;
        IsSettingFlag = true;
        Debug.Log("[GManager] Setting �Ϸ�: ���� = " + m_userObj.name);
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
        else
        {
            Debug.LogError("[GManager] FadeFadeInOut ���� �� ��!");
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
            Debug.LogWarning("[GManager] �� �׷� �Ǵ� ī�޶� null�̾ ���� ���� ���� ����");
            return;
        }

        var collider = currentMapGroup.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            Debug.LogWarning("[GManager] currentMapGroup�� BoxCollider2D�� �����ϴ�");
            return;
        }

        Bounds bounds = collider.bounds;
        IsCameraBase.SetCameraBounds(bounds.min, bounds.max);

        Debug.Log($"[GManager] ī�޶� ���� ���� ���� �Ϸ�: min={bounds.min}, max={bounds.max}");
    }
    public void AutoReferenceSceneObjects()
    {
        currentMapGroup = GameObject.Find("MapM1_Sub01");
        m_fadeInOut = FindObjectOfType<FadeInOut>();
        m_cameraBase = FindObjectOfType<CameraBase>();
        m_potionCraftUI = FindObjectOfType<PotionCraftUI>();
        m_shopUI = FindObjectOfType<ShopUI>();
        m_inventoryUI = GameObject.Find("Inventory")?.GetComponent<InventoryUI>();
        m_invenManager = FindObjectOfType<InventoryManager>();
        m_UIManager = FindObjectOfType<UIManager>();
        m_craftUI = FindObjectOfType<CraftUI>();
        m_exchangeManager = FindObjectOfType<ExchangeManager>();
        IsUserController = FindObjectOfType<UserController>();

        // UIManager ������ GameObject �ʵ嵵 �ڵ� ����
        if (m_UIManager != null)
        {
            m_UIManager.CraftUI = GameObject.Find("CraftUI");
            m_UIManager.PotionCraftUI = GameObject.Find("PotionCraftUI");
            m_UIManager.ShopUI = GameObject.Find("ShopUI");
            Debug.Log("[Debug] CraftUI: " + GameObject.Find("CraftUI"));
            Debug.Log("[Debug] ShopUI: " + GameObject.Find("ShopUI"));
        }
    }



}
