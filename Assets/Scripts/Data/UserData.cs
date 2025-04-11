using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UserData", menuName = "Data/UserData", order = 1)]

public class UserData : ScriptableObject
{
    /// <summary>
    /// ���� ���
    /// </summary>
    public int m_gold;

    /// <summary>
    /// ���ݼ��� ���
    /// </summary>
    [SerializeField] GradeType.Type m_grade = GradeType.Type.Novice;

    public GradeType.Type IsGrade { get { return m_grade; } }


}
