using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private Image m_goodsIcon;
    [SerializeField] private GameObject m_highlight;

    private ItemData m_goodsData;

    public ItemData GetItemData() => m_goodsData;

    public void Set(ItemData item)
    {
        if (item == null)
        {
            m_goodsIcon.enabled = false;
            Debug.LogWarning("[ShopSlot] Set() ȣ�������� item�� null�Դϴ�.");
            return;
        }

        m_goodsData = item;

        m_goodsIcon.enabled = true;
        m_goodsIcon.sprite = item.m_itemIcon;
        m_highlight.SetActive(false);

        //  ����� �α�
        Debug.Log($"[ShopSlot] ���� ���� �Ϸ� - ������: {item.m_itemName}");
    }

    public void Clear()
    {
        m_goodsData = null;
        m_goodsIcon.sprite = null;
        m_goodsIcon.enabled = false;

        //  ����� �α�
        Debug.Log("[ShopSlot] ���� Ŭ���� �Ϸ�");
    }

    public void SetHighlight(bool isActive)
    {
        m_highlight.SetActive(isActive);

        //  ����� �α�
        Debug.Log($"[ShopSlot] ���̶���Ʈ {(isActive ? "Ȱ��ȭ" : "��Ȱ��ȭ")}");
    }
}
