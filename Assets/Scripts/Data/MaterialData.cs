using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialData", menuName = "Data/MaterialData", order = 1)]

public class MaterialData : ScriptableObject
{
    public string m_MaterialName;
    /// <summary>
    /// �ʵ� ������Ʈ ��������Ʈ �ε���
    /// 0: ä�� ��/ 1: ä�� ��
    /// </summary>
    public List<Sprite> stateSprites;
    /// <summary>
    /// ���̵� UI �̸�
    /// </summary>
    public string m_objName = null;

    /// <summary>
    /// ������ ������
    /// </summary>
    public ItemData Item;

    /// <summary>
    /// ä����
    /// </summary>
    public int amount = 1;
}
