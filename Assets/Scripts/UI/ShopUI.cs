using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
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


    private float holdDelay = 0.2f;
    private float holdTimer = 0f;
    private enum TabType { Purchase, Sell }
    private TabType currentTab = TabType.Purchase;
    private int selectedIndex = 0;


    private void Update()
    {
        if (!gameObject.activeSelf) return;
        HandleSlotMoveInput();
        // �� ��ȯ
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchTab();
            //currentTab = (currentTab == TabType.Purchase) ? TabType.Sell : TabType.Purchase;
            //SwitchTab(currentTab);
        }
        // ���� ���� (ZŰ)
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
        // 1. ���� GetKeyDown ó�� (ó�� ���� ������ ����)
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveSlot(-1);
            holdTimer = holdDelay;
            return; //  �ߺ� �Է� ����
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveSlot(1);
            holdTimer = holdDelay;
            return;
        }

        // 2. ������ �ִ� ���� ó�� (Hold)
        if (Input.GetKey(KeyCode.W))
        {
            holdTimer -= Time.deltaTime;
            if (holdTimer <= 0f)
            {
                MoveSlot(-1);
                holdTimer = holdDelay;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            holdTimer -= Time.deltaTime;
            if (holdTimer <= 0f)
            {
                MoveSlot(1);
                holdTimer = holdDelay;
            }
        }
        else
        {
            holdTimer = 0f;
        }
    }
    void MoveSlot(int dir)
    {
        selectedIndex = Mathf.Clamp(selectedIndex + dir, 0, slotList.Count - 1);

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
    }

    private void UpdatePurchaseUI()
    {
        // 1. ���� ���� �� ����
        foreach (var slot in m_purchaseSlotList)
        {
            Destroy(slot.gameObject);
        }
        m_purchaseSlotList.Clear();
        Debug.Log("[ShopUI] ���� ���� ���� ��� ���� �Ϸ�");

        // 2. �Ǹ� ������ ����Ʈ�� ���鼭 ���� ����
        foreach (var item in m_shopGoodsList)
        {
            if (item != null)
            {
                ShopSlot slot = Instantiate(m_purchaseSlotPrefab, m_purchaseSlotGroup);
                slot.Set(item);
                m_purchaseSlotList.Add(slot);

                //  ����� �α� �߰�
                Debug.Log($"[ShopUI] ���� ���� ���� �Ϸ� - ������: {item.m_itemName}");
            }
            else
            {
                //  �������� null�̸� ��� �α�
                Debug.LogWarning("[ShopUI] m_shopGoodsList�� null �׸��� �ֽ��ϴ�.");
            }
        }

        Debug.Log($"[ShopUI] ���� ���� ���� �Ϸ� - �� {m_purchaseSlotList.Count}�� ������");
    }



    private void UpdateSellUI()
    {
        var data = GManager.Instance.IsinvenManager.IsInventoryData;

        for (int i = 0; i < m_sellSlot.Length; i++)
        {
            if (i < data.slots.Length && data.slots[i] != null && data.slots[i].itemData != null)
            {
                m_sellSlot[i].SetSlot(data.slots[i].itemData, data.slots[i].quantity);
            }
            else
            {
                m_sellSlot[i].ClearSlot();
            }
        }
    }

}
