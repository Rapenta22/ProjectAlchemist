using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialData", menuName = "Data/MaterialData", order = 1)]

public class MaterialData : ScriptableObject
{
    /// <summary>
    /// 필드 오브젝트 스프라이트
    /// </summary>
    public Sprite m_fieldSprite;
    /// <summary>
    /// 채집된 스프라이트
    /// </summary>
    public Sprite m_gainedSprite;
    /// <summary>
    /// 가이드 UI 이름
    /// </summary>
    [SerializeField] public string m_objName = null;

}
