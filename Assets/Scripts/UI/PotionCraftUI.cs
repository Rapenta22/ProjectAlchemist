using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionCraftUI : MonoBehaviour
{


    [SerializeField] private ExchangeManager m_exchangeManager;
    [Header("���� ���� �ڽ� (�̹���)")]

    [SerializeField] private Slot m_Input1Slot;
    [SerializeField] private Slot m_Input2Slot;
    [SerializeField] private Slot m_OutputSlot;

    /// <summary>
    /// ������(���� UI)
    /// </summary>
    [SerializeField] private Image m_potionIllust;
    [SerializeField] private TMP_Text m_potionName;


    [SerializeField] private GameObject m_potionCraftUI;

    [Header("�� ǥ�ÿ� ������Ʈ (�̹��� ��)")]
    [SerializeField] private TMP_Text NTab;
    [SerializeField] private TMP_Text ETab;
    [SerializeField] private TMP_Text MTab;
    [SerializeField] private Color m_selectedColor = Color.black;
    [SerializeField] private Color m_defaultColor = new Color32(161, 116, 37, 255);


    [Header("��ũ�� ����Ʈ")]
    [SerializeField] private Transform contentRoot;
    [SerializeField] private GameObject PotionItemPrefab;

    [Header("�Ǻ� ���� ������")]
    [SerializeField] private List<PotionCraftData> NPotionList;
    [SerializeField] private List<PotionCraftData> EPotionList;
    [SerializeField] private List<PotionCraftData> MPotionList;

    [Header("����ȭ��)")]
    [SerializeField] private GameObject m_bookCraftUI;
    [SerializeField] private TMP_Text m_pNameInCraft;
    [SerializeField] private TMP_Text m_potionText;
    [SerializeField] private Image m_PIllustInCraft;

    [SerializeField] private Image m_Input1InCraft;
    [SerializeField] private Image m_Input2InCraft;
    [SerializeField] private TMP_Text m_Input1Name;
    [SerializeField] private TMP_Text m_Input2Name;





    [Header("����Ʈ ǥ�� �θ�")]
    [SerializeField] private Transform PotionListContent;
    [SerializeField] private ScrollRect scrollRect;
    private float scrollStepY = 70f;     // ���� �ϳ� ����


    private enum TabType { Novice, Expert, Master }
    private TabType currentTab = TabType.Novice;

    private List<PotionCraftListUI> slotList = new List<PotionCraftListUI>();
    private int selectedIndex = 0;
    public int GetSelectedIndex()=>selectedIndex;
    private enum UIState { List, Craft }
    private UIState currentState = UIState.List;

    private float holdDelay = 0.2f;
    private float holdTimer = 0f;

    void Start()
    {
        m_bookCraftUI.SetActive(false);
        m_potionCraftUI.SetActive(false);
        InitPotionUI();
        SwitchTab(TabType.Novice);
        SetupCraftList();
        HighlightSlot();
        autoFindExchangeManager();

    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        // �� ��ȯ
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // �������: Novice �� Expert �� Master �� �ٽ� Novice
            switch (currentTab)
            {
                case TabType.Novice:
                    currentTab = TabType.Expert;
                    break;
                case TabType.Expert:
                    currentTab = TabType.Master;
                    break;
                case TabType.Master:
                    currentTab = TabType.Novice;
                    break;
            }

            SwitchTab(currentTab);
        }
        HandleSlotMoveInput();
       
        // ZŰ ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentState == UIState.List)
            {
                TrySelected(); // ����Ʈ �� �� ���� ��ȯ
            }
            else if (currentState == UIState.Craft)
            {
                TryCraft(); // �� �������� ���� �õ�
            }
        }
        if (!gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Z) && currentState == UIState.Craft)
        {
            currentState = UIState.List;
            m_bookCraftUI.SetActive(false);

            // ���� �ʱ�ȭ
            m_Input1Slot.Clear();
            m_Input2Slot.Clear();
            m_OutputSlot.Clear();

            Debug.Log("[PotionCraftUI] ����Ʈ ���� ���� - �� �ڽ� �ʱ�ȭ��");
        }

    }
    void SwitchTab(TabType tab)
    {
        currentTab = tab;
        SetupCraftList();
        selectedIndex = 0;
        HighlightSlot();
        UpdateTabVisual();


        // �� UI ǥ�� ó���� ������ ���⿡ �߰�
    }
    public void autoFindExchangeManager()
    {
        m_exchangeManager = FindObjectOfType<ExchangeManager>();
    }
    /// <summary>
    /// �� UI ���� ������Ʈ
    /// </summary>
    private void UpdateTabVisual()
    {
        NTab.color = (currentTab == TabType.Novice) ? m_selectedColor : m_defaultColor;
        ETab.color = (currentTab == TabType.Expert) ? m_selectedColor : m_defaultColor;
        MTab.color = (currentTab == TabType.Master) ? m_selectedColor : m_defaultColor;
    }
    /// <summary>
    /// ���� ���̶���Ʈ ó��
    /// </summary>
    private void HighlightSlot()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            slotList[i].SetHighlight(i == selectedIndex);
        }

        ScrollToSelected(); // ���õ� �׸� �ڵ� ��Ŀ��
        UpdateDescriptionUI();

    }
    /// <summary>
    /// ��ũ�Ѻ信�� ���õ� �������� ��ũ�� �̵�
    /// </summary>
    private void ScrollToSelected()
    {
        if (scrollRect == null || slotList.Count == 0) return;

        int visibleMidIndex = 2; // 0���� �����ϹǷ� 3��°�� �ε��� 2
        float viewportHeight = scrollRect.viewport.rect.height;
        float contentHeight = scrollRect.content.rect.height;
        float maxScrollY = contentHeight - viewportHeight;

        float scrollY = 0f;

        if (selectedIndex > visibleMidIndex)
        {
            scrollY = (selectedIndex - visibleMidIndex) * scrollStepY;
            scrollY = Mathf.Min(scrollY, maxScrollY); // �ʹ� �������� �ʵ��� ����
        }

        scrollRect.content.anchoredPosition = new Vector2(
            scrollRect.content.anchoredPosition.x,
            scrollY
        );
    }
    /// <summary>
    /// ����Ʈ ����
    /// </summary>
    private void SetupCraftList()
    {
        foreach (Transform child in PotionListContent)
            Destroy(child.gameObject);

        slotList.Clear();

        switch (currentTab)
        {
            case TabType.Novice:
                AddItemsLot(NPotionList);
                break;
            case TabType.Expert:
                AddItemsLot(EPotionList);
                break;
            case TabType.Master:
                AddItemsLot(MPotionList);
                break;
        }
    }
    /// <summary>
    /// ����Ʈ ����
    /// </summary>
    /// <param name="argPotionList">���Ǹ���Ʈ�迭</param>
    public void AddItemsLot(List<PotionCraftData> argPotionList)
    {
        foreach (var data in argPotionList)
        {
            var obj = Instantiate(PotionItemPrefab, PotionListContent);
            var itemUI = obj.GetComponent<PotionCraftListUI>();
            if (itemUI != null)
            {
                itemUI.Setup(data);
                slotList.Add(itemUI);
            }
        }
    }
    /// <summary>
    /// ����Ʈ���� ���õ� ������ ���� ������ ��������
    /// </summary>
    void TrySelected()
    {
        var selected = GetSelectedSlot();
        if (selected == null) return;

        var data = selected.GetPotionData();
        if (data == null) return;
        // BookCraft UI ǥ��
        m_bookCraftUI.SetActive(true);

        // ���� ����
        m_Input1Slot.Set(data.IsInputI1, data.IsIAmount1);
        m_Input2Slot.Set(data.IsInputI2, data.IsIAmount2);
        m_OutputSlot.Set(data.IsOutputItem, data.IsOAmount);

        // ����(����) ����
        m_PIllustInCraft.sprite=data.IsPotionIllust;
        m_potionText.text = data.IsPotionDS;
        m_pNameInCraft.text = data.IsName;
        m_Input1InCraft.sprite = data.IsInputI1.m_itemIcon;
        m_Input2InCraft.sprite = data.IsInputI2.m_itemIcon;
        m_Input1Name.text = data.IsInputI1.m_itemName;
        m_Input2Name.text = data.IsInputI2.m_itemName;

        Debug.Log($"[TrySelected] ���õ� �ε���: {selectedIndex}");


        currentState = UIState.Craft;
        Debug.Log($"[PotionCraftUI] �� ��� ����: {data.IsName}");
    }


    public PotionCraftListUI GetSelectedSlot()
    {
        if (slotList.Count == 0 || selectedIndex < 0 || selectedIndex >= slotList.Count)
            return null;
        return slotList[selectedIndex];
    }
    void TryCraft()
    {
        if (selectedIndex < 0 || selectedIndex >= slotList.Count)
        {
            Debug.LogWarning("[TryCraft] �߸��� �ε��� ����: selectedIndex = " + selectedIndex);
            return;
        }

        PotionCraftListUI selected = slotList[selectedIndex];
        PotionCraftData data = selected.GetPotionData();
        if (data == null)
        {
            Debug.LogWarning("[TryCraft] ���õ� ���� �����Ͱ� null�Դϴ�.");
            return;
        }

        Debug.Log($"[TryCraft] ���� ���� �õ�: {data.IsName}");

        // 1. ��� Ȯ��
        if (m_exchangeManager.m_userData.IsGrade < data.IsGradeType)
        {
            Debug.LogWarning($"[TryCraft] ���� ����: ��� ���� (����: {m_exchangeManager.m_userData.IsGrade}, �ʿ�: {data.IsGradeType})");
            return;
        }

        // 2. ���1 üũ
        if (!m_exchangeManager.InvenManager.IsInventoryData.HasItem(data.IsInputI1, data.IsIAmount1))
        {
            Debug.LogWarning($"[TryCraft] ���� ����: ���1 ���� - {data.IsInputI1.m_itemName} �ʿ�: {data.IsIAmount1}");
            return;
        }

        // 3. ���2 üũ
        if (!m_exchangeManager.InvenManager.IsInventoryData.HasItem(data.IsInputI2, data.IsIAmount2))
        {
            Debug.LogWarning($"[TryCraft] ���� ����: ���2 ���� - {data.IsInputI2.m_itemName} �ʿ�: {data.IsIAmount2}");
            return;
        }

        // 4. �κ��丮 ���� üũ
        if (!m_exchangeManager.InvenManager.IsInventoryData.HasSpaceForItem(data.IsOutputItem, data.IsOAmount))
        {
            Debug.LogWarning($"[TryCraft] ���� ����: �κ��丮 ���� ���� (������: {data.IsOutputItem.m_itemName}, ����: {data.IsOAmount})");
            return;
        }

        // ���� ����
        m_exchangeManager.PotionCraft(data);
        Debug.Log($"[TryCraft] ���� ����! {data.IsOutputItem.m_itemName} �� {data.IsOAmount}");
    }


    // ���� ���� �̵�
    private void HandleSlotMoveInput()
    {
        // 1. ���� GetKeyDown ó�� (ó�� ���� ������ ����)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSlot(-1);
            holdTimer = holdDelay;
            return; //  �ߺ� �Է� ����
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSlot(1);
            holdTimer = holdDelay;
            return;
        }

        // 2. ������ �ִ� ���� ó�� (Hold)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            holdTimer -= Time.deltaTime;
            if (holdTimer <= 0f)
            {
                MoveSlot(-1);
                holdTimer = holdDelay;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
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
        Debug.Log($"[MoveSlot] ���� ���� �ε���: {selectedIndex}");
        HighlightSlot();
    }

    public void InitPotionUI()
    {
        currentTab = TabType.Novice;     // �� �ʱ�ȭ
        selectedIndex = 0;               // �ε��� �ʱ�ȭ
        currentState = UIState.List;     // ���� �ʱ�ȭ

        m_bookCraftUI.SetActive(false);  // �� ȭ�� ��Ȱ��ȭ �߰�
        m_Input1Slot.Clear();            // ���� ����
        m_Input2Slot.Clear();
        m_OutputSlot.Clear();

        SetupCraftList();                // ����Ʈ ����
        UpdateDescriptionUI();
        SwitchTab(currentTab);           // �ǿ� �´� ����Ʈ ����
        HighlightSlot();                 // ù ��° ���� ���� + ��ũ�� ����
    }

    public void UpdateDescriptionUI()
    {
        if (selectedIndex < 0 || selectedIndex >= slotList.Count)
            return;

        if (currentTab == TabType.Novice)
        {
            var selected = GetSelectedSlot();
            var data = selected.GetPotionData();
            m_potionName.text = data.IsName;
            m_potionIllust.sprite = data.IsPotionIllust;
        }
        else if (currentTab == TabType.Expert)
        {
            var selected = GetSelectedSlot();
            var data = selected.GetPotionData();
            m_potionName.text = data.IsName;
            m_potionIllust.sprite = data.IsPotionIllust;
        }
        else if (currentTab == TabType.Master)
        {
            var selected = GetSelectedSlot();
            var data = selected.GetPotionData();
            m_potionName.text = data.IsName;
            m_potionIllust.sprite = data.IsPotionIllust;
        }
    }
}
