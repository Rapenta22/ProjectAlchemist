using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : ParentController
{
    [SerializeField] private float m_moveSpeed = 5f; // 이동 속도
    private Vector2 m_input = Vector2.zero;
    private Rigidbody2D m_rb;
    private int wallLayer; // 벽 레이어 저장
    private float lastMoveX = 1f; // 처음에는 왼쪽 보는 걸로 가정
    [SerializeField] private Animator animator;
    bool m_InvenFlag = false;
    /// <summary>
    /// 인터렉터블인터페이스
    /// </summary>
    private IInteractableInterface currentTarget = null;
    /// <summary>
    /// 인터렉션 감지 범위
    /// </summary>
    [SerializeField] private float m_interactRange = 1.5f;
    private List<Collider2D> interactablesInRangeList = new List<Collider2D>();

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        wallLayer = LayerMask.NameToLayer("Wall"); // "Wall" 레이어 가져오기

        // Rigidbody2D 설정
        m_rb.gravityScale = 0;
        m_rb.freezeRotation = true;
        m_rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        Interact();
        Move();
    }
    public override void Move()
    {
        if (GManager.Instance.IsUIManager.UIOpenFlag) return;

        // 입력값 가져오기
        m_input.x = Input.GetAxisRaw("Horizontal");
        m_input.y = Input.GetAxisRaw("Vertical");
        m_input = m_input.normalized; // 대각선 이동 속도 보정
        
        bool isMove = m_input.magnitude > 0.1f;
        animator.SetBool("isMove", isMove);

        // 이동 중일 때만 방향 갱신
        if (isMove)
        {
            if (Mathf.Abs(m_input.x) > 0.1f) // 좌우 입력이 있을 때만
            {
                lastMoveX = m_input.x > 0 ? 1f : -1f;
            }

            // 이동 중이든 아니든 현재 방향 반영 (중요!)
            animator.SetFloat("moveX", lastMoveX);
        }
    }

    private void FixedUpdate()
    {
        // Rigidbody2D를 사용하여 이동
        m_rb.velocity = m_input * m_moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 벽체크
        if (collision.gameObject.layer == wallLayer)
        {
            Debug.Log("벽과 충돌! 이동 차단됨.");
        }
    }

    /// <summary>
    /// 아이템 윈도우 관련
    /// </summary>
    void ItemView()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InventoryOpenClose();
        }
    }
    /// <summary>
    /// 아이템박스 열고 닫기
    /// </summary>
    public void InventoryOpenClose()
    {
        m_InvenFlag = !m_InvenFlag;
        switch (m_InvenFlag)
        {
            case true:
                //GManager.Instance.m_InvenFlag.OpenWindow(m_saveData);
                break;
            case false:
                //GManager.Instance.m_InvenFlag.CloseWindow();
                break;
        }
    }

    /// <summary>
    /// 충돌체크
    /// </summary>
    /// <param name="other">충돌한 콜라이더</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[TriggerEnter] 충돌 감지: {other.gameObject.name}");

        var interactable = other.GetComponent<IInteractableInterface>();
        if (interactable != null)
        {
            Debug.Log($"[TriggerEnter] 상호작용 가능한 오브젝트 감지됨: {other.gameObject.name}");

            if (!interactablesInRangeList.Contains(other))
            {
                interactablesInRangeList.Add(other);
                Debug.Log($"[TriggerEnter] 리스트에 추가됨: {other.gameObject.name}");
            }
        }
        else
        {
            Debug.Log($"[TriggerEnter] IInteractableInterface 없음: {other.gameObject.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"[TriggerExit] 충돌 해제: {other.gameObject.name}");

        if (interactablesInRangeList.Contains(other))
        {
            interactablesInRangeList.Remove(other);
            Debug.Log($"[TriggerExit] 리스트에서 제거됨: {other.gameObject.name}");
        }
    }

    /// <summary>
    /// 인터렉션 인터페이스 
    /// </summary>
    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (interactablesInRangeList.Count == 0)
            {
                return;
            }

            foreach (var col in interactablesInRangeList)
            {
                Debug.Log($"[Interact] 대상 확인 중: {col.gameObject.name}");

                var target = col.GetComponent<IInteractableInterface>();
                if (target != null)
                {
                    Debug.Log($"[Interact] 상호작용 시도: {col.gameObject.name}");
                    target.Interact();
                    break;
                }
                else
                {
                    Debug.Log($"[Interact] 인터페이스 없음: {col.gameObject.name}");
                }
            }
        }
    }

}
