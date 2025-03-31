using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private int nowQuantity;

    /// <summary>
    /// 슬롯 세팅
    /// </summary>
    public void SetSlot(ItemData itemData, int quantity)
    {
        icon.sprite = itemData.m_itemIcon;
        icon.enabled = true;
        nowQuantity = quantity;
        quantityText.text = quantity > 1 ? quantity.ToString() : "";
    }

    /// <summary>
    /// 슬롯 비우기
    /// </summary>
    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        nowQuantity = 0;
        quantityText.text = "";
    }

    /// <summary>
    /// 현재 수량 가져오기
    /// </summary>
    public int GetQuantity()
    {
        return nowQuantity;
    }
}
