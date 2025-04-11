using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
[CreateAssetMenu(fileName = "PotionCraftData", menuName = "Data/PotionCraftData", order = 1)]

public class PotionCraftData : ScriptableObject
{
    [SerializeField] GradeType.Type m_gradeType = GradeType.Type.Novice;
    [SerializeField] ItemType.Type m_itemType = ItemType.Type.Potion;
    [SerializeField] string m_pName = null;
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
    [SerializeField] int m_OAmount;
    /// <summary>
    /// ��� �Ҹ�
    /// </summary>
    public int m_IAmount1;
    [SerializeField] int m_IAmount2;

    /// <summary>
    /// �Ϸ���Ʈ �̹���
    /// </summary>
    [SerializeField] Sprite m_potionIllust = null;
    public Sprite IsPotionIllust { get { return m_potionIllust; } } 
    /// <summary>
    /// ���� ����
    /// </summary>
    [SerializeField] [TextArea] string m_PotionDS;
    public string IsPotionDS { get { return m_PotionDS; } }

    public ItemData IsInputI1 { get { return m_inputI1; } }
    public ItemData IsInputI2 { get { return m_inputI2; } }
    public ItemData IsOutputItem { get { return m_outputItemData; } }

    public int IsOAmount { get { return m_OAmount; } }

    public int IsIAmount1 { get { return m_IAmount1; } }

    public int IsIAmount2 { get { return m_IAmount2; } }
    public int IsIndex { get { return m_index; } }
    public ItemType.Type IsItemType { get { return m_itemType; } }

    public GradeType.Type IsGradeType { get { return m_gradeType; } }

    public string IsName { get { return m_pName; } }


}
