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
    /// <summary>
    /// ���ͷ��ͺ��������̽�
    /// </summary>
    private IInteractableInterface currentTarget = null;

    private List<Collider2D> interactablesInRangeList = new List<Collider2D>();

    public bool IsMoveLock { get; private set; } = false;


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
        if (GManager.Instance != null && GManager.Instance.TPFlag) return;

        Interact();
        Move();
    }
    public override void Move()
    {
        if (GManager.Instance.IsInventoryUI.isOpen) return;
        if (GManager.Instance.IsUIManager.UIOpenFlag) return;
        if (GManager.Instance != null && GManager.Instance.TPFlag) return;
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
    /// <summary>
    /// �浹üũ
    /// </summary>
    /// <param name="other">�浹�� �ݶ��̴�</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractableInterface>();
        if (interactable != null)
        {
            if (!interactablesInRangeList.Contains(other))
            {
                interactablesInRangeList.Add(other);
                Debug.Log($"[TriggerEnter] ����Ʈ�� �߰���: {other.gameObject.name}");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (interactablesInRangeList.Contains(other))
        {
            interactablesInRangeList.Remove(other);
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
            }
        }
    }
    public void ResetMoveAndAnimation()
    {
        m_input = Vector2.zero; // �Է� �ʱ�ȭ

        if (animator != null)
        {
            animator.SetFloat("moveX", 0f); //  ��Ȯ�� �Ķ���� �̸�
            animator.SetBool("isMove", false); //  ��Ȯ�� �Ķ���� �̸�
        }
    }
}
