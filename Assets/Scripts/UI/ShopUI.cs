using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    [SerializeField] private GameObject m_shopUI;
    [SerializeField] private ExchangeManager m_exchangeManager;
    [SerializeField] private List<ItemData> m_shopGoodsList;
    [SerializeField] private InventorySlot[] m_sellSlot;
    [SerializeField] private ShopSlot m_purchaseSlotPrefab;  // ���� ������
    [SerializeField] private Transform m_purchaseSlotGroup;       // ���� �θ�
    private List<ShopSlot> m_purchaseSlotList = new List<ShopSlot>(); // ������ ���� ����Ʈ


    private List<CraftListUI> slotList = new List<CraftListUI>();

    [Header("�� ����")]
    [SerializeField] private Image m_purchaseTab;
    [SerializeField] private Image m_sellTab;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private GameObject m_sellGroup;
    [SerializeField] private GameObject m_purchaseGroup;

    private int currentIndex = 0; // ���� ���� ����

    private float holdDelay = 0.5f;    // ó�� ������ �� ���� �ݺ����� ������
    private float repeatRate = 0.1f;   // ���� �Է� ����
    private float holdTimer = 0f;

    private enum TabType { Purchase, Sell }
    private TabType currentTab = TabType.Purchase;
    private int selectedIndex = 0;

    void Start()
    {
        m_shopUI.SetActive(false);
        InitShopUI();
        SwitchTab(TabType.Purchase);
        UpdatePurchaseUI();

    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;
        HandleSlotMoveInput();
        // �� ��ȯ
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchTab();
        }
        //�׼�
        if (Input.GetKeyDown(KeyCode.Z))
        {
        }
    }
    void SwitchTab(TabType tab)
    {
        selectedIndex = 0;
        //SetupList();
        UpdateTabSprites();
    }

    private void HandleSlotMoveInput()
    {
        // �ʱ�ȭ
        holdTimer -= Time.deltaTime;

        // W
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
            holdTimer = holdDelay;
            return;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && holdTimer <= 0f)
        {
            MoveUp();
            holdTimer = repeatRate;
            return;
        }

        // S
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
            holdTimer = holdDelay;
            return;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && holdTimer <= 0f)
        {
            MoveDown();
            holdTimer = repeatRate;
            return;
        }

        // A
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
            holdTimer = holdDelay;
            return;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && holdTimer <= 0f)
        {
            MoveLeft();
            holdTimer = repeatRate;
            return;
        }

        // D
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
            holdTimer = holdDelay;
            return;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && holdTimer <= 0f)
        {
            MoveRight();
            holdTimer = repeatRate;
            return;
        }

        // Ű �� ������ ������ �ʱ�ȭ
        if (!Input.GetKey(KeyCode.UpArrow) &&
            !Input.GetKey(KeyCode.DownArrow) &&
            !Input.GetKey(KeyCode.LeftArrow) &&
            !Input.GetKey(KeyCode.RightArrow))
        {
            holdTimer = 0f;
        }
        UpdateSlotSelection();

    }


    /// <summary>
    /// �� ��ȯ �̹��� ����
    /// </summary>
    private void UpdateTabSprites()
    {
        m_purchaseTab.sprite = (currentTab == TabType.Purchase) ? selectedSprite : defaultSprite;
        m_sellTab.sprite = (currentTab == TabType.Sell) ? selectedSprite : defaultSprite;
    }
    public void SwitchTab()
    {
        // 1. �� ��ȯ
        currentTab = (currentTab == TabType.Purchase) ? TabType.Sell : TabType.Purchase;

        // 2. ���� �׷� �����ֱ�
        if (currentTab == TabType.Purchase)
        {
            m_purchaseGroup.SetActive(true);
            m_sellGroup.SetActive(false);

            UpdatePurchaseUI(); // ������ ������Ʈ
        }
        else if (currentTab == TabType.Sell)
        {
            m_purchaseGroup.SetActive(false);
            m_sellGroup.SetActive(true);

            UpdateSellUI(); // �Ǹ��� ������Ʈ
        }

        // 3. �� ��ư ���־� ������Ʈ
        UpdateTabSprites();
        selectedIndex = 0;
        UpdateSlotSelection(); // ���� UI ���ŵ� ���� ���ִ� �� ����
    }

    private void UpdatePurchaseUI()
    {
        // 1. ���� ���� �� ����
        foreach (var slot in m_purchaseSlotList)
        {
            Destroy(slot.gameObject);
        }
        m_purchaseSlotList.Clear();
        // 2. �Ǹ� ������ ����Ʈ�� ���鼭 ���� ����
        foreach (var item in m_shopGoodsList)
        {
            if (item != null)
            {
                ShopSlot slot = Instantiate(m_purchaseSlotPrefab, m_purchaseSlotGroup);
                slot.Set(item);
                m_purchaseSlotList.Add(slot);
            }
        }
    }
    private void UpdateSellUI()
    {
        var data = GManager.Instance.IsinvenManager.IsInventoryData;

        if (data == null || data.slots == null)return;
        for (int i = 0; i < m_sellSlot.Length; i++)
        {
            if (i < data.slots.Length)
            {
                var slotData = data.slots[i];

                if (slotData == null)
                {
                }
                else if (slotData.itemData == null)
                {
                }
                else
                {
                    m_sellSlot[i].SetSlot(slotData.itemData, slotData.quantity);
                }
            }
        }

    }
    public void InitShopUI()
    {
        currentTab = TabType.Purchase;
        selectedIndex = 0;

        m_purchaseGroup.SetActive(true); //  ���� ���־� �ʱ�ȭ
        m_sellGroup.SetActive(false);    //  ���� ���� ����

        SwitchTab(currentTab);           // ���� ���� ȣ��
    }
    private void MoveLeft()
    {
        int rowSize = 8;
        int rowStart = (selectedIndex / rowSize) * rowSize;
        selectedIndex = (selectedIndex == rowStart) ? rowStart + rowSize - 1 : selectedIndex - 1;
    }

    private void MoveRight()
    {
        int rowSize = 8;
        int rowStart = (selectedIndex / rowSize) * rowSize;
        selectedIndex = (selectedIndex == rowStart + rowSize - 1) ? rowStart : selectedIndex + 1;
    }

    private void MoveUp()
    {
        if (selectedIndex - 8 >= 0)
            selectedIndex -= 8;
    }

    private void MoveDown()
    {
        int totalCount = GetCurrentSlotCount();
        if (selectedIndex + 8 < totalCount)
            selectedIndex += 8;
    }


private int GetCurrentSlotCount()
{
    return currentTab == TabType.Purchase ? m_purchaseSlotList.Count : m_sellSlot.Length;
}
    private void UpdateSlotSelection()
    {
        if (currentTab == TabType.Purchase)
        {
            for (int i = 0; i < m_purchaseSlotList.Count; i++)
            {
                bool isSelected = i == selectedIndex;
                m_purchaseSlotList[i].SetSelected(isSelected);
                if (isSelected)
                    Debug.Log($"[����] ���� ���� {i} ���õ�: {m_purchaseSlotList[i].GetItemName()}");
            }
        }
        else if (currentTab == TabType.Sell)
        {
            for (int i = 0; i < m_sellSlot.Length; i++)
            {
                bool isSelected = i == selectedIndex;
                m_sellSlot[i].SetSelected(isSelected);
                if (isSelected)
                    Debug.Log($"[����] �Ǹ� ���� {i} ���õ�: {m_sellSlot[i].GetItemName()}");
            }
        }
    }

}
