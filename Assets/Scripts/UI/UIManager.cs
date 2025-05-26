using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject CraftUI;
    public GameObject PotionCraftUI;
    public GameObject ShopUI;
    public GameObject DialogueUI;
    public GameObject BookUI;

    /// <summary>
    /// �÷��� ����
    /// </summary>
    public bool UIOpenFlag
    {
        get
        {
            return CraftUIOpenFlag
                || PotionCraftUIOpenFlag
                || DialogueOpenFlag
                || ShopUIOpenFlag
                || BookUIOpenFlag
                || GManager.Instance.IsInventoryUI.isOpen;

        }
    }
    public bool CraftUIOpenFlag = false;
    public bool PotionCraftUIOpenFlag = false;
    public bool DialogueOpenFlag = false;
    public bool ShopUIOpenFlag = false;
    public bool BookUIOpenFlag = false;
    /*void Awake()
    {
        if (CraftUI == null) CraftUI = GameObject.Find("CraftUI");
        if (PotionCraftUI == null) PotionCraftUI = GameObject.Find("PotionCraftUI");
        if (ShopUI == null) ShopUI = GameObject.Find("ShopUI");
        if (DialogueUI == null) DialogueUI = GameObject.Find("DialogueUI");
    }
    */

    void Update()
    {
        HandleBookUIOpen();
        CloseUI();
    }
    public void OpenCraftUI()
    {
        CraftUIOpenFlag = true;
        CraftUI.SetActive(true);
        GManager.Instance.IsCraftUI.InitCraftUI();
    }
    public void OpenPotionCraftUI()
    {
        PotionCraftUIOpenFlag = true;
        PotionCraftUI.SetActive(true);
        GManager.Instance.IsPotionCraftUI.InitPotionUI();

    }
    public void OpenShopUI()
    {
        ShopUIOpenFlag = true;
        ShopUI.SetActive(true);
        GManager.Instance.IsShopUI.InitShopUI();
    }
    public void OpenBookUI()
    {
        BookUIOpenFlag = true;
        BookUI.SetActive(true);
//        GManager.Instance.IsBookUI.InitBookUI();
    }

    public void OpenDialogueUI(DialogueNode startNode)
    {
        DialogueOpenFlag = true;

        if (DialogueUI == null)
        {
            Debug.LogError("[UIManager] DialogueUI�� ������� �ʾҽ��ϴ�.");
            return;
        }

        DialogueUI.SetActive(true);

        if (startNode == null)
        {
            Debug.LogWarning("[UIManager] startNode�� null�Դϴ�.");
            return;
        }

        GManager.Instance.IsDialogueManager.StartDialogue(startNode);
    }

    private void HandleBookUIOpen()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // BookUI�� ���� ���������� ����, �ƴϸ� ����
            if (!BookUIOpenFlag)
            {
                OpenBookUI();
            }
            else
            {
                return;
            }
        }
    }

    public void CloseUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CraftUIOpenFlag)
            {
                CraftUIOpenFlag = false;
                CraftUI.SetActive(false);
            }
            if (PotionCraftUIOpenFlag)
            {
                PotionCraftUIOpenFlag = false;
                PotionCraftUI.SetActive(false);
            }
            if (ShopUIOpenFlag)
            {
                ShopUIOpenFlag = false;
                ShopUI.SetActive(false);
            }
            // �ٸ� UI�鵵 ���� ���⿡ �߰�
            if (DialogueOpenFlag)
            {
                DialogueOpenFlag = false;
                DialogueUI.SetActive(false);
            }
            if (BookUIOpenFlag)
            {
                BookUIOpenFlag = false;
                BookUI.SetActive(false);
            }
            GManager.Instance.IsUserController.isInteracting = false; // ���ͷ��� ���� ���·� ����

        }
    }
}
