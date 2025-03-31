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


    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        wallLayer = LayerMask.NameToLayer("Wall"); // "Wall" 레이어 가져오기

        // Rigidbody2D 설정
        m_rb.gravityScale = 0;
        m_rb.freezeRotation = true;
        m_rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
    public override void Move()
    {
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
        Debug.Log($"[Collision Detected] 충돌 오브젝트: {collision.gameObject.name}, Layer: {collision.gameObject.layer}");

        // 벽(Wall) 레이어와 충돌한 경우만 처리
        if (collision.gameObject.layer == wallLayer)
        {
            Debug.Log("벽과 충돌! 이동 차단됨.");
        }
        else
        {
            Debug.Log("벽이 아닌 다른 오브젝트와 충돌했습니다.");
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
}
