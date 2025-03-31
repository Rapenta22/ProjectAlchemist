using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentController : MonoBehaviour
{
    public bool IsMoveFlag { get; set; } = false;

    /// <summary>
    /// �ִϸ�����
    /// </summary>
    public Animator IsAnimator { get; set; } = null;
    /// <summary>
    /// �̵�
    /// </summary>
    public virtual void Move() { }

    /// <summary>
    /// ��ȣ�ۿ�
    /// </summary>
    public virtual void InterAction() { }

    /// <summary>
    /// �ִϸ��̼� �÷���
    /// </summary>
    public virtual void AniPlay()
    {
    }

    void Update()
    {
        if (!IsMoveFlag)
        {
            Move();  // �ڽ� Ŭ�������� Move()�� ����
        }
    }
}
