using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] public GameObject Inven;
    [SerializeField] public InventorySlot[] m_invenSlot;
    [SerializeField] public InventorySlot[] m_quickSlot;

    [SerializeField] public bool isOpen = false;

    private int currentInventoryIndex = 0; // �κ��丮 ���� ���� ����

    private int currentQuickIndex = 0;


    public void Start()
    {
        Inven.SetActive(false);
        GManager.Instance.SetInventoryUI(this); // GManager���� ���� ����
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { 
            OpenInventory();
        }
        HandleQuickSlotInput();
        if (isOpen)
        {
            HandleInventoryInput();
        }
    }

    public void OpenInventory()
    {
        isOpen = !isOpen;
        Inven.SetActive(isOpen);
        Debug.Log($"�κ��丮 ����: {isOpen}");

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

    private void HandleQuickSlotInput()
    {
        for (int i = 0; i < 8; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetQuickSlotIndex(i);
            }
        }
    }

    private void SetQuickSlotIndex(int index)
    {
        currentQuickIndex = index;
        Debug.Log($"[������] {index + 1}�� ���� ���õ�");

        // ���� ���̶���Ʈ ����
        for (int i = 0; i < m_quickSlot.Length; i++)
        {
            if (i == index)
            {
                m_quickSlot[i].SetSelected(true); // ���õ� ���� ǥ��
            }
            else
            {
                m_quickSlot[i].SetSelected(false);
            }
        }
    }
    private void HandleInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        }

        UpdateInventorySlotSelection();
    }
    private void MoveLeft()
    {
        int rowStart = (currentInventoryIndex / 8) * 8; // ���� �� ���� ��ȣ

        if (currentInventoryIndex == rowStart)
            currentInventoryIndex = rowStart + 7; // �� ù ��° �����̸� �� ����������
        else
            currentInventoryIndex -= 1;
    }
    private void MoveRight()
    {
        int rowStart = (currentInventoryIndex / 8) * 8; // ���� �� ���� ��ȣ

        if (currentInventoryIndex == rowStart + 7)
            currentInventoryIndex = rowStart; // �� ������ �����̸� �� ù ��°��
        else
            currentInventoryIndex += 1;
    }
    private void MoveUp()
    {
        if (currentInventoryIndex - 8 >= 0)
            currentInventoryIndex -= 8;
        else
        {
            // �� �����̸� �׳� ����
        }
    }
    private void MoveDown()
    {
        if (currentInventoryIndex + 8 < m_invenSlot.Length)
            currentInventoryIndex += 8;
        else
        {
            // �� �Ʒ����̸� �׳� ����
        }
    }
    private void UpdateInventorySlotSelection()
    {
        for (int i = 0; i < m_invenSlot.Length; i++)
        {
            if (i == currentInventoryIndex)
                m_invenSlot[i].SetSelected(true); // ����
            else
                m_invenSlot[i].SetSelected(false); // ����
        }
    }
}

