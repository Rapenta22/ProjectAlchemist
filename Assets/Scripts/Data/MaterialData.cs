using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialData", menuName = "Data/MaterialData", order = 1)]

public class MaterialData : ScriptableObject
{
    /// <summary>
    /// �ʵ� ������Ʈ ��������Ʈ
    /// </summary>
    public Sprite m_fieldSprite;
    /// <summary>
    /// ä���� ��������Ʈ
    /// </summary>
    public Sprite m_gainedSprite;
    /// <summary>
    /// ���̵� UI �̸�
    /// </summary>
    [SerializeField] public string m_objName = null;

}
