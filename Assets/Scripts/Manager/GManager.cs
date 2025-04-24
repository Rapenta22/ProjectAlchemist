using FunkyCode.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public MapBGMController mapBGMController;
    [SerializeField] private SoundManager m_soundManager;
    public SoundManager IsSoundManager { get { return m_soundManager; } }

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
        InitFirstMapBounds();
        Debug.Log($"[GManager] mapBGMController ���� ����: {(mapBGMController != null ? "����" : "null")}");

        if (currentMapGroup != null && mapBGMController != null)
        {
            mapBGMController.PlayBGMForMap(currentMapGroup);
        }
        else
        {
            Debug.LogWarning("[GManager] �ʱ� ���̳� mapBGMController�� �������� ����");
        }
    }
    public void SetInventoryUI(InventoryUI ui)
    {
        m_inventoryUI = ui; 
    }

    public void SetTPFlag(bool isOn)
    {
        TPFlag = isOn;
        Debug.Log($"[TPFlag] ���� ����� �� {(isOn ? "ON (�̵� �Ұ�)" : "OFF (�̵� ����)")}");
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
        Debug.Log("[GManager] ���̵� �� ����");

        // �ٷ� FadeIn ����
        yield return StartCoroutine(m_fadeInOut.FadeIn());

        Debug.Log("[GManager] ���̵� �� �Ϸ�, TPFlag OFF");

        SetTPFlag(false);
    }
    private void InitFirstMapBounds()
    {
        if (currentMapGroup == null)
        {
            Debug.LogWarning("[GManager] ������ �� currentMapGroup�� ����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        BoxCollider2D collider = currentMapGroup.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            Debug.LogWarning("[GManager] ������ �� currentMapGroup�� BoxCollider2D�� �����ϴ�.");
            return;
        }

        Bounds bounds = collider.bounds;
        Vector2 min = bounds.min;
        Vector2 max = bounds.max;

        if (IsCameraBase != null)
        {
            IsCameraBase.SetCameraBounds(min, max);
            Debug.Log($"[GManager] ���� ���� �� ī�޶� ���� �ڵ� ����: Min {min} / Max {max}");
        }
        else
        {
            Debug.LogError("[GManager] CameraBase ������ �Ǿ� ���� �ʽ��ϴ�.");
        }
    }
}
