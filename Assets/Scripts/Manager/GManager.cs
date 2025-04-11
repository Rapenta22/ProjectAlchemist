using FunkyCode.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{

    [SerializeField] private PotionCraftUI m_potionCraftUI;
    public PotionCraftUI IsPotionCraftUI { get { return m_potionCraftUI; } }


    /// <summary>
    /// 유저
    /// </summary>
    [SerializeField] private UserController m_user = null;
    /// <summary>
    /// 유저 게임 오브젝트
    /// </summary>
    GameObject m_userObj = null;
    /// <summary>
    /// 인벤토리
    /// </summary>
    public InventoryUI InventoryUI { get; set; }
    /// <summary>
    /// 인벤 매니저
    /// </summary>
    public InventoryManager IsinvenManager { get { return m_invenManager; } }
    /// <summary>
    /// 인벤토리 매니저
    /// </summary>
    [SerializeField] InventoryManager m_invenManager = null;
    public InventoryManager IsInvenManager { get; set; }
    /// <summary>
    /// UI매니저
    /// </summary>
    [SerializeField] UIManager m_UIManager = null;
    public UIManager IsUIManager { get { return m_UIManager; } }

    /// <summary>
    /// 제작 UI
    /// </summary>
    [SerializeField] CraftUI m_craftUI = null;

    public CraftUI IsCraftUI { get { return m_craftUI; } }
    /// <summary>
    /// 교환 매니저
    /// </summary>
    [SerializeField] ExchangeManager m_exchangeManager = null;
    public ExchangeManager IsExchangeManager { get {return  m_exchangeManager;} }




    /// <summary>
    /// 세팅 플래그
    /// 씬 전환시 false로
    /// </summary>
    public bool IsSettingFlag { get; set; } = false;


    /// <summary>
    /// 싱글톤 인스턴스
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
    /// 세팅
    /// </summary>
    /// <param name="argUserObj">유저 오브젝트</param>
    public void Setting(GameObject argUserObj)
    {
        m_userObj = argUserObj;
        IsSettingFlag = true;
    }
    /// <summary>
    /// 유저 컨트롤러
    /// </summary>
    public UserController IsUserController { get; private set; } = null;




    /// <summary>
    /// 유저 트렌스폼
    /// </summary>
    public Transform IsUserTrans
    {
        get { return m_userObj != null ? m_userObj.transform : null; }
    }

    private void Start()
    {
        //  Start()에서 자동으로 Setting() 호출
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
