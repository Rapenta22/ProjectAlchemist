using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject player;  // 플레이어 오브젝트
    public GameObject newObjectPrefab; // 교체할 오브젝트 프리팹
    [SerializeField] private float interactionDistance = 1.0f; // 상호작용 거리
    private Transform objsParent; // Objs 부모 오브젝트

    /// <summary>
    /// 해당 아이템의 인덱스
    /// </summary>
    [SerializeField] private int m_index = 0;

    private void Start()
    {
        // Objs라는 이름의 부모 오브젝트 찾기
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

        // 플레이어와 오브젝트 사이의 거리 계산
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // 특정 거리 내에서 스페이스바를 누르면 오브젝트 교체
        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.Space))
        {
            SwitchObject();
        }
    }

    void SwitchObject()
    {
        // 현재 오브젝트의 위치와 회전값 저장
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        // 기존 오브젝트의 sortingOrder 저장
        int sortingOrder = 0;
        SpriteRenderer currentRenderer = GetComponent<SpriteRenderer>();
        if (currentRenderer != null)
        {
            sortingOrder = currentRenderer.sortingOrder;
        }

        // 기존 오브젝트 삭제
        Destroy(gameObject);

        // 새로운 오브젝트 생성
        GameObject newObject = Instantiate(newObjectPrefab, currentPosition, currentRotation);

        // 부모를 Objs로 설정
        if (objsParent != null)
        {
            newObject.transform.SetParent(objsParent);
        }
        else
        {
        }

        // 새로운 오브젝트의 sortingOrder 설정
        SpriteRenderer newRenderer = newObject.GetComponent<SpriteRenderer>();
        if (newRenderer != null)
        {
            newRenderer.sortingOrder = sortingOrder;
        }

        // 인벤토리에 넘겨주기
        GManager.Instance.IsInvenManager.UpdateInven(1);

    }
}
