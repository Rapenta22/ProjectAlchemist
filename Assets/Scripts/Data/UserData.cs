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
    public GradeType m_grade;


}
