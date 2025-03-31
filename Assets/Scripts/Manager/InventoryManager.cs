using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public InventoryData inventoryData { get; private set; }
    [SerializeField] private int inventorySize = 24;

    private void Awake()
    {
        inventoryData = new InventoryData(inventorySize);
    }

    public void AddItem(ItemData item, int amount = 1)
    {
        inventoryData.AddItem(item, amount);
        GManager.Instance.InventoryUI.UpdateUI(); // 추가
    }

    public void RemoveItem(ItemData item, int amount = 1)
    {
        inventoryData.RemoveItem(item, amount);
        GManager.Instance.InventoryUI.UpdateUI(); // 추가
    }

}

