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
    /// <summary>
    /// ���ͷ��ͺ��������̽�
    /// </summary>
    private IInteractableInterface currentTarget = null;
    /// <summary>
    /// ���ͷ��� ���� ����
    /// </summary>
    [SerializeField] private float m_interactRange = 1.5f;
    private List<Collider2D> interactablesInRangeList = new List<Collider2D>();

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        wallLayer = LayerMask.NameToLayer("Wall"); // "Wall" ���̾� ��������

        // Rigidbody2D ����
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
        // ��üũ
        if (collision.gameObject.layer == wallLayer)
        {
            Debug.Log("���� �浹! �̵� ���ܵ�.");
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

    /// <summary>
    /// �浹üũ
    /// </summary>
    /// <param name="other">�浹�� �ݶ��̴�</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[TriggerEnter] �浹 ����: {other.gameObject.name}");

        var interactable = other.GetComponent<IInteractableInterface>();
        if (interactable != null)
        {
            Debug.Log($"[TriggerEnter] ��ȣ�ۿ� ������ ������Ʈ ������: {other.gameObject.name}");

            if (!interactablesInRangeList.Contains(other))
            {
                interactablesInRangeList.Add(other);
                Debug.Log($"[TriggerEnter] ����Ʈ�� �߰���: {other.gameObject.name}");
            }
        }
        else
        {
            Debug.Log($"[TriggerEnter] IInteractableInterface ����: {other.gameObject.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"[TriggerExit] �浹 ����: {other.gameObject.name}");

        if (interactablesInRangeList.Contains(other))
        {
            interactablesInRangeList.Remove(other);
            Debug.Log($"[TriggerExit] ����Ʈ���� ���ŵ�: {other.gameObject.name}");
        }
    }

    /// <summary>
    /// ���ͷ��� �������̽� 
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
                Debug.Log($"[Interact] ��� Ȯ�� ��: {col.gameObject.name}");

                var target = col.GetComponent<IInteractableInterface>();
                if (target != null)
                {
                    Debug.Log($"[Interact] ��ȣ�ۿ� �õ�: {col.gameObject.name}");
                    target.Interact();
                    break;
                }
                else
                {
                    Debug.Log($"[Interact] �������̽� ����: {col.gameObject.name}");
                }
            }
        }
    }

}
