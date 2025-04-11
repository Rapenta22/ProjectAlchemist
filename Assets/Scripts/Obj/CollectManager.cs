using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringObject : MonoBehaviour, InterAct.IInteractable
{
    [SerializeField] private GameObject player;
    [SerializeField] private float interactionDistance = 1.0f; // ��ȣ�ۿ� �Ÿ�
    private Transform objsParent; // Objs �θ� ������Ʈ

    [SerializeField] private MaterialData data;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isCollected = false;

    /// <summary>
    /// �ش� �������� �ε��� (���� ��� ����)
    /// </summary>
    //[SerializeField] private int m_index = 0;

    private void Start()
    {


        // ��������Ʈ ����
        if (data.stateSprites.Count >= 1)
        {
            spriteRenderer.sprite = data.stateSprites[0];
        }
        else
        {
        }
    }


    private void Update()
    {
        if (isCollected) return;

        if (player == null)
        {
            return;
        }

        // �Ÿ� üũ
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.Space))
        {
            Collect();
        }
    }
    public void Interact()
    {
        // ��ȣ�ۿ� ����
        if (isCollected)
        {
            return;
        }
        isCollected = true;
        Collect();
    }
    public void Collect()
    {
        if (isCollected)
        {
            return;
        }
        isCollected = true;
        // ��������Ʈ ����
        if (data.stateSprites.Count >= 2)
        {
            spriteRenderer.sprite = data.stateSprites[1];
        }
        else
        {
        }

        // �κ��丮 �߰�
        if (GManager.Instance.IsinvenManager != null)
        {
            GManager.Instance.IsinvenManager.AddItem(data.m_itemData, data.amount);
        }
        else
        {
        }

    }
}
