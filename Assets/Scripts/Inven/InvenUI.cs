using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject Inven;
    [SerializeField] private InventorySlot[] m_invenSlot;
    [SerializeField] private InventorySlot[] m_quickSlot;

    private bool isOpen = false;

    private void Start()
    {
        Inven.SetActive(false);
        GManager.Instance.SetInventoryUI(this); // GManager에서 참조 가능
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        Inven.SetActive(isOpen);
    }


    public void UpdateUI()
    {
        var data = GManager.Instance.IsinvenManager.inventoryData;

        for (int i = 0; i < m_invenSlot.Length; i++)
        {
            if (i < data.slots.Length && data.slots[i] != null)
            {
                m_invenSlot[i].SetSlot(data.slots[i].itemData, data.slots[i].quantity);
            }
            else
            {
                m_invenSlot[i].ClearSlot();
            }
        }
        for (int i = 0; i < m_quickSlot.Length; i++)
        {
            if (i < data.slots.Length && data.slots[i] != null)
            {
                m_quickSlot[i].SetSlot(data.slots[i].itemData, data.slots[i].nowQuantity);
            }
            else
            {
                m_quickSlot[i].ClearSlot();
            }
        }
    }
}

