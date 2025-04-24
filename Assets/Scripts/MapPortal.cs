using UnityEngine;
using System.Collections;

public class MapPortal : MonoBehaviour
{
    [Header("�̵��� ��ǥ ��ġ")]
    [SerializeField] private GameObject targetObject;

    [Header("���� �� / ���� ��")]
    [SerializeField] private GameObject currentMapGroup;
    [SerializeField] private GameObject nextMapGroup;

    private bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(TeleportRoutine(collision.gameObject));
        }
    }

    private IEnumerator TeleportRoutine(GameObject player)
    {
        isTeleporting = true;

        GManager.Instance.SetTPFlag(true);

        // �̵� ������ �� �Է�/��� ����
        UserController userController = player.GetComponent<UserController>();
        if (userController != null)
        {
            userController.ResetMoveAndAnimation();
        }

        // ���̵� �ƿ�
        yield return StartCoroutine(GManager.Instance.IsFadeInOut.FadeOut());

        // ���� �� ��Ȱ��ȭ
        if (currentMapGroup != null)
            currentMapGroup.SetActive(false);

        // ���� �� Ȱ��ȭ
        if (nextMapGroup != null)
        {
            nextMapGroup.SetActive(true);
            Debug.Log($"[MapPortal] ���� �� Ȱ��ȭ: {nextMapGroup.name}");

            // ī�޶� ���� �ڵ� ���
            SetCameraBoundsByNextMap();

            // BGM �ڵ� ��� (�� �̸� ����)
            GManager.Instance.mapBGMController.PlayBGMForMap(nextMapGroup);
        }
        else
        {
            Debug.LogWarning("[MapPortal] nextMapGroup�� null�Դϴ�!");
        }


        if (targetObject != null)
            player.transform.position = targetObject.transform.position;

        GManager.Instance.StartTPAfterTeleport();

        isTeleporting = false;
    }

    /// <summary>
    /// ���� ���� BoxCollider2D�� ī�޶� ���� �ڵ� ����
    /// </summary>
    private void SetCameraBoundsByNextMap()
    {
        if (nextMapGroup == null) return;

        BoxCollider2D collider = nextMapGroup.GetComponent<BoxCollider2D>();

        if (collider != null)
        {
            Bounds bounds = collider.bounds;

            Vector2 min = bounds.min;
            Vector2 max = bounds.max;

            GManager.Instance.IsCameraBase.SetCameraBounds(min, max);
            Debug.Log($"[MapPortal] ī�޶� ���� �ڵ� ����: Min {min} / Max {max}");
        }
        else
        {
            Debug.LogWarning("[MapPortal] ���� �ʿ� BoxCollider2D�� �����ϴ�! ī�޶� ���� ���� ����");
        }
    }
}
