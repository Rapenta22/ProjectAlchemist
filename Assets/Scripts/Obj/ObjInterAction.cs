using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject player;  // �÷��̾� ������Ʈ
    public GameObject newObjectPrefab; // ��ü�� ������Ʈ ������
    [SerializeField] private float interactionDistance = 1.0f; // ��ȣ�ۿ� �Ÿ�
    private Transform objsParent; // Objs �θ� ������Ʈ

    /// <summary>
    /// �ش� �������� �ε���
    /// </summary>
    [SerializeField] private int m_index = 0;

    private void Start()
    {
        // Objs��� �̸��� �θ� ������Ʈ ã��
        GameObject objs = GameObject.Find("Objs");
        if (objs != null)
        {
            objsParent = objs.transform;
        }
        else
        {
        }
    }

    private void Update()
    {
        if (player == null || newObjectPrefab == null)
        {
            return;
        }

        // �÷��̾�� ������Ʈ ������ �Ÿ� ���
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Ư�� �Ÿ� ������ �����̽��ٸ� ������ ������Ʈ ��ü
        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.Space))
        {
            SwitchObject();
        }
    }

    void SwitchObject()
    {
        // ���� ������Ʈ�� ��ġ�� ȸ���� ����
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        // ���� ������Ʈ�� sortingOrder ����
        int sortingOrder = 0;
        SpriteRenderer currentRenderer = GetComponent<SpriteRenderer>();
        if (currentRenderer != null)
        {
            sortingOrder = currentRenderer.sortingOrder;
        }

        // ���� ������Ʈ ����
        Destroy(gameObject);

        // ���ο� ������Ʈ ����
        GameObject newObject = Instantiate(newObjectPrefab, currentPosition, currentRotation);

        // �θ� Objs�� ����
        if (objsParent != null)
        {
            newObject.transform.SetParent(objsParent);
        }
        else
        {
        }

        // ���ο� ������Ʈ�� sortingOrder ����
        SpriteRenderer newRenderer = newObject.GetComponent<SpriteRenderer>();
        if (newRenderer != null)
        {
            newRenderer.sortingOrder = sortingOrder;
        }

        // �κ��丮�� �Ѱ��ֱ�
        GManager.Instance.IsInvenManager.UpdateInven(1);

    }
}
