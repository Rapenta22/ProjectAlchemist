using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
[CreateAssetMenu(fileName = "OilCraftData", menuName = "Data/OilCraftData", order = 1)]

public class OilCraftData:ScriptableObject
{

    [SerializeField] ItemType.Type m_itemType = ItemType.Type.Material;
    [SerializeField] string m_Name = null;

    [SerializeField] int m_index = 0;
    /// <summary>
    /// ��� ������ ������
    /// </summary>
    [SerializeField] ItemData m_inputI1 = null;
    /// <summary>
    /// ��� ������ ������2
    /// </summary>
    [SerializeField] ItemData m_inputI2 = null;
    /// <summary>
    /// ����� ������ ������
    /// </summary>
    [SerializeField] ItemData m_outputItemData = null;
    /// <summary>
    /// ����� ���۷�
    /// </summary>
    [SerializeField] int m_outputAmount;
    /// <summary>
    /// ��� �Ҹ�
    /// </summary>
    [SerializeField] int m_IAmount1;
    [SerializeField] int m_IAmount2;

    public ItemType.Type IsItemType { get { return m_itemType; } }
    public ItemData IsInputI1 { get { return m_inputI1; } }
    public ItemData IsInputI2 { get { return m_inputI2; } }
    public ItemData IsOutputItem { get { return m_outputItemData; } }

    public int IsOAmount { get { return m_outputAmount; } }

    public int IsIAmount1 { get { return m_IAmount1; } }

    public int IsIAmount2 { get { return m_IAmount2; } }

    public int IsIndex { get { return m_index; } }
}
