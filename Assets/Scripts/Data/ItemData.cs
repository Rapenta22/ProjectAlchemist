using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item Data", order = 1)]

public class ItemData : ScriptableObject
{
    [SerializeField] string m_itemName;
    public int m_maxStack;
    public Sprite m_itemIcon;
    public ItemType ItemType;
    [TextArea] public string description;
    public bool m_usableItem;


}
