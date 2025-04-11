using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] public GameObject Inven;
    [SerializeField] public InventorySlot[] m_invenSlot;
    [SerializeField] public InventorySlot[] m_quickSlot;

    [SerializeField] public bool isOpen = false;

    public void Start()
    {
        Inven.SetActive(false);
        GManager.Instance.SetInventoryUI(this); // GManager에서 참조 가능
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { 
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        isOpen = !isOpen;
        Inven.SetActive(isOpen);
        Debug.Log($"인벤토리 상태: {isOpen}");

    }


    public void UpdateUI()
    {
        var data = GManager.Instance.IsinvenManager.IsInventoryData;

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

