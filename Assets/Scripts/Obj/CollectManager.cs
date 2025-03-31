using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float interactionDistance = 1.0f; // 상호작용 거리
    private Transform objsParent; // Objs 부모 오브젝트

    [SerializeField] private MaterialData data;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isCollected = false;

    /// <summary>
    /// 해당 아이템의 인덱스 (현재 사용 안함)
    /// </summary>
    //[SerializeField] private int m_index = 0;

    private void Start()
    {
        Debug.Log($"[CollectManager] '{gameObject.name}' 초기화 시작");

        // Objs 부모 찾기
        GameObject objs = GameObject.Find("Objs");
        if (objs != null)
        {
            objsParent = objs.transform;
            Debug.Log("[CollectManager] Objs 부모 오브젝트 연결 완료");
        }
        else
        {
            Debug.LogWarning("[CollectManager] Objs 부모 오브젝트를 찾을 수 없습니다");
        }

        // 스프라이트 세팅
        if (data.stateSprites.Count >= 1)
        {
            spriteRenderer.sprite = data.stateSprites[0];
            Debug.Log($"[CollectManager] '{data.m_MaterialName}' 채집 전 스프라이트 적용 완료");
        }
        else
        {
            Debug.LogWarning($"[CollectManager] '{data.m_MaterialName}' 스프라이트 데이터가 부족합니다");
        }
    }

    private void Update()
    {
        if (isCollected) return;

        if (player == null)
        {
            Debug.LogWarning("[CollectManager] 플레이어가 할당되지 않았습니다");
            return;
        }

        // 거리 체크
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"[CollectManager] '{gameObject.name}' 채집 시도");
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
        // 스프라이트 변경
        if (data.stateSprites.Count >= 2)
        {
            spriteRenderer.sprite = data.stateSprites[1];
        }
        else
        {
        }

        // 인벤토리 추가
        if (GManager.Instance.IsinvenManager != null)
        {
            GManager.Instance.IsinvenManager.AddItem(data.Item, data.amount);
        }
        else
        {
            Debug.LogError("[CollectManager] InventoryManager 인스턴스가 없습니다");
        }

    }
}
