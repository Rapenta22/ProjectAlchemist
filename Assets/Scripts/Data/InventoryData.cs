using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlotData
{
    public ItemData itemData;
    public int quantity;
    public int nowQuantity => quantity;


    public InventorySlotData(ItemData data, int quantity)
    {
        this.itemData = data;
        this.quantity = quantity;
    }

    public bool IsFull()
    {
        return quantity >= itemData.m_maxStack;
    }
}

[System.Serializable]
public class InventoryData
{
    public InventorySlotData[] slots;

    public InventoryData(int size)
    {
        slots = new InventorySlotData[size];
    }

    public void AddItem(ItemData data, int amount = 1)
    {
        // 1. 기존 슬롯에 추가
        for (int i = 0; i < slots.Length; i++)
        {
            var slot = slots[i];
            if (slot != null && slot.itemData == data && !slot.IsFull())
            {
                int space = data.m_maxStack - slot.quantity;
                int addAmount = Mathf.Min(space, amount);
                slot.quantity += addAmount;
                amount -= addAmount;
                if (amount <= 0) return;
            }
        }

        // 2. 빈 슬롯에 추가
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                int addAmount = Mathf.Min(data.m_maxStack, amount);
                slots[i] = new InventorySlotData(data, addAmount);
                amount -= addAmount;
                if (amount <= 0) return;
            }
        }

        if (amount > 0)
        {
            Debug.LogWarning("[InventoryData] 인벤토리 가득참. 남은 아이템: " + amount);
        }
    }

    public void RemoveItem(ItemData data, int amount = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var slot = slots[i];
            if (slot != null && slot.itemData == data)
            {
                slot.quantity -= amount;
                if (slot.quantity <= 0)
                    slots[i] = null;
                return;
            }
        }
    }
}
