using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : ParentController
{
    [SerializeField] private float m_moveSpeed = 5f; // �̵� �ӵ�
    private Vector2 m_input = Vector2.zero;
    private Rigidbody2D m_rb;
    private int wallLayer; // �� ���̾� ����
    private float lastMoveX = 1f; // ó������ ���� ���� �ɷ� ����
    [SerializeField] private Animator animator;
    bool m_InvenFlag = false;


    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        wallLayer = LayerMask.NameToLayer("Wall"); // "Wall" ���̾� ��������

        // Rigidbody2D ����
        m_rb.gravityScale = 0;
        m_rb.freezeRotation = true;
        m_rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
    public override void Move()
    {
        // �Է°� ��������
        m_input.x = Input.GetAxisRaw("Horizontal");
        m_input.y = Input.GetAxisRaw("Vertical");
        m_input = m_input.normalized; // �밢�� �̵� �ӵ� ����
        
        bool isMove = m_input.magnitude > 0.1f;
        animator.SetBool("isMove", isMove);

        // �̵� ���� ���� ���� ����
        if (isMove)
        {
            if (Mathf.Abs(m_input.x) > 0.1f) // �¿� �Է��� ���� ����
            {
                lastMoveX = m_input.x > 0 ? 1f : -1f;
            }

            // �̵� ���̵� �ƴϵ� ���� ���� �ݿ� (�߿�!)
            animator.SetFloat("moveX", lastMoveX);
        }
    }

    private void FixedUpdate()
    {
        // Rigidbody2D�� ����Ͽ� �̵�
        m_rb.velocity = m_input * m_moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"[Collision Detected] �浹 ������Ʈ: {collision.gameObject.name}, Layer: {collision.gameObject.layer}");

        // ��(Wall) ���̾�� �浹�� ��츸 ó��
        if (collision.gameObject.layer == wallLayer)
        {
            Debug.Log("���� �浹! �̵� ���ܵ�.");
        }
        else
        {
            Debug.Log("���� �ƴ� �ٸ� ������Ʈ�� �浹�߽��ϴ�.");
        }
    }

    /// <summary>
    /// ������ ������ ����
    /// </summary>
    void ItemView()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InventoryOpenClose();
        }
    }
    /// <summary>
    /// �����۹ڽ� ���� �ݱ�
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
