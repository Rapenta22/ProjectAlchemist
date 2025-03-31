using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private int nowQuantity;

    /// <summary>
    /// ���� ����
    /// </summary>
    public void SetSlot(ItemData itemData, int quantity)
    {
        icon.sprite = itemData.m_itemIcon;
        icon.enabled = true;
        nowQuantity = quantity;
        quantityText.text = quantity > 1 ? quantity.ToString() : "";
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        nowQuantity = 0;
        quantityText.text = "";
    }

    /// <summary>
    /// ���� ���� ��������
    /// </summary>
    public int GetQuantity()
    {
        return nowQuantity;
    }
}
