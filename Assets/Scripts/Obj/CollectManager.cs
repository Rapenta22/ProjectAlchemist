using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
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
        Debug.Log($"[CollectManager] '{gameObject.name}' �ʱ�ȭ ����");

        // Objs �θ� ã��
        GameObject objs = GameObject.Find("Objs");
        if (objs != null)
        {
            objsParent = objs.transform;
            Debug.Log("[CollectManager] Objs �θ� ������Ʈ ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning("[CollectManager] Objs �θ� ������Ʈ�� ã�� �� �����ϴ�");
        }

        // ��������Ʈ ����
        if (data.stateSprites.Count >= 1)
        {
            spriteRenderer.sprite = data.stateSprites[0];
            Debug.Log($"[CollectManager] '{data.m_MaterialName}' ä�� �� ��������Ʈ ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning($"[CollectManager] '{data.m_MaterialName}' ��������Ʈ �����Ͱ� �����մϴ�");
        }
    }

    private void Update()
    {
        if (isCollected) return;

        if (player == null)
        {
            Debug.LogWarning("[CollectManager] �÷��̾ �Ҵ���� �ʾҽ��ϴ�");
            return;
        }

        // �Ÿ� üũ
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"[CollectManager] '{gameObject.name}' ä�� �õ�");
            Collect();
        }
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
            GManager.Instance.IsinvenManager.AddItem(data.Item, data.amount);
        }
        else
        {
            Debug.LogError("[CollectManager] InventoryManager �ν��Ͻ��� �����ϴ�");
        }

    }
}
