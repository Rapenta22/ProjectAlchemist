using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentController : MonoBehaviour
{
    public bool IsMoveFlag { get; set; } = false;

    /// <summary>
    /// 오브젝트 사용 플래그
    /// </summary>
    bool IsActiveFlag = false;

    /// <summary>
    /// 대화 플래그
    /// </summary>
    bool IsTalkFlag = false;

    /// <summary>
    /// 액션 플래그
    /// </summary>
    bool IsActionFlag = false;

    /// <summary>
    /// 애니메이터
    /// </summary>
    public Animator IsAnimator { get; set; } = null;
    /// <summary>
    /// 이동
    /// </summary>
    public virtual void Move() { }

    /// <summary>
    /// 상호작용
    /// </summary>
    public virtual void InterAction() { }

    /// <summary>
    /// 애니메이션 플레이
    /// </summary>
    public virtual void AniPlay()
    {
    }

    void Update()
    {
        if (!IsMoveFlag)
        {
            Move();  // 자식 클래스에서 Move()를 구현
        }
    }
}
