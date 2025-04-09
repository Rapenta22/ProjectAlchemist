using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    /// <summary>
    /// 유저 게임 오브젝트
    /// </summary>
    GameObject m_userObj = null;

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
    /// 유저 트랜스폼 반환 (null 체크 추가)
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
        //  Start()에서 자동으로 Setting() 호출
        GameObject m_character = GameObject.Find("Character");
        if (m_character != null)
        {
            Setting(m_character);
        }
    }


}
