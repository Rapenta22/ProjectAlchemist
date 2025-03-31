using FunkyCode.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    /// <summary>
    /// 유저 게임 오브젝트
    /// </summary>
    GameObject m_userObj = null;

    public InventoryUI InventoryUI { get; private set; }

    [SerializeField] InventoryManager m_invenManager = null;

    /// <summary>
    /// 세팅 플래그
    /// 씬 전환시 false로
    /// </summary>
    public bool IsSettingFlag { get; set; } = false;

    public InventoryManager IsInvenManager { get; private set; } = null;

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

    /// <summary>
    /// 세이브/로드 매니저
    /// </summary>
    public InventoryManager IsinvenManager { get { return m_invenManager; } }

    public void SetInventoryUI(InventoryUI ui)
    {
        InventoryUI = ui;
    }
}
