using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// 획득 가능한 아이템 데이터 리스트
    /// </summary>
    [SerializeField] List <ItemData> m_dataList = null;

    public void UpdateInven(int argIndex)
    {
        // .TO do : 인덱스 받아온 걸로 인벤에 넣기 
    }
} 
