using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObsMap : MonoBehaviour
{
    /// <summary>
    /// Ÿ�ϸ�
    /// </summary>
    [SerializeField] private Tilemap m_tileMap  = null;

    /// <summary>
    /// Ÿ�ϸ� ��ȯ
    /// </summary>
    public Tilemap IsGet { get { return m_tileMap; } }
}
