[System.Serializable]
public class InventorySlotData
{
    public ItemData itemData;
    public int quantity;

    public InventorySlotData(ItemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
    }
}
