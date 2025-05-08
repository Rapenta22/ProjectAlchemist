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
    /// <summary>
    /// �̵� �÷��� 
    /// </summary>
    public bool m_moveFlag = false;
    public bool IsMoveLock { get; private set; } = false;

    public bool isInteracting = false;


    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        wallLayer = LayerMask.NameToLayer("Wall"); // "Wall" ���̾� ��������
        // Rigidbody2D ����
        m_rb.gravityScale = 0;
        m_rb.freezeRotation = true;
        m_rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        SetMoveFlag(true); // �� ���ʿ� �� �� �̵� �����ϰ� ����

    }

    void Update()
    {
        if (GManager.Instance != null && GManager.Instance.TPFlag) return;
        Interact();
        Move();
    }
    private void FixedUpdate()
    {
        if (!m_moveFlag)
        {
            m_rb.velocity = Vector2.zero;
            return;
        }

        m_rb.velocity = m_input * m_moveSpeed;
    }

    public override void Move()
    {
        if (!m_moveFlag)
        {
            m_input = Vector2.zero;
            animator.SetBool("isMove", false);
            return;
        }
        if (GManager.Instance == null)
        {
            Debug.LogError("[UserController] GManager.Instance�� null�Դϴ�.");
            return;
        }

        if (GManager.Instance.IsInventoryUI == null)
        {
            Debug.LogError("[UserController] IsInventoryUI�� null�Դϴ�.");
            return;
        }

        if (GManager.Instance.IsInventoryUI.isOpen)
        {
            Debug.Log("[UserController] �κ��丮 UI�� ���� �־� �̵� �Ұ�.");
            m_input = Vector2.zero;
            m_rb.velocity = Vector2.zero;
            animator.SetBool("isMove", false);
            return;
        }

        if (GManager.Instance.IsUIManager.UIOpenFlag)
        {
            m_input = Vector2.zero;
            m_rb.velocity = Vector2.zero;
            animator.SetBool("isMove", false);
            return;
        }

        if (GManager.Instance.TPFlag)
        {
            m_input = Vector2.zero;
            m_rb.velocity = Vector2.zero;
            animator.SetBool("isMove", false);
            return;
        }

        // �Ʒ��� �Է� ���� ó��
        m_input.x = Input.GetAxisRaw("Horizontal");
        m_input.y = Input.GetAxisRaw("Vertical");
        m_input = m_input.normalized;

        bool isMove = m_input.magnitude > 0.1f;
        animator.SetBool("isMove", isMove);

        if (isMove)
        {
            if (Mathf.Abs(m_input.x) > 0.1f)
            {
                lastMoveX = m_input.x > 0 ? 1f : -1f;
            }
            animator.SetFloat("moveX", lastMoveX);
        }
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
    public void SetMoveFlag(bool m_moveFlag)
    {
        this.m_moveFlag = m_moveFlag;

        if (!m_moveFlag)
        {
            m_input = Vector2.zero;          
            m_rb.velocity = Vector2.zero;     
            animator.SetBool("isMove", false);
        }
    }
    /// <summary>
    /// ���ͷ��� �������̽� 
    /// </summary>
    private void Interact()
    {
        if (isInteracting) return; // �ߺ� ���ͷ��� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isInteracting = true;

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
        m_input = Vector2.zero;

        if (animator != null)
        {
            animator.SetBool("isMove", false);
        }
    }
}
