using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Inventory m_Inventory;
    public QuickBar m_QuickBar;
    public Items m_ItemType;

    public HandObjects m_HandObjects;
    private int cacheSlotIndex = 99;

    private void Start()
    {
        m_Inventory = GetComponent<Inventory>();
        m_QuickBar = FindObjectOfType<QuickBar>();
        m_HandObjects = FindObjectOfType<HandObjects>();
    }

    private void Update()
    {
        if (!m_QuickBar)
        {
            FindObjectOfType<QuickBar>();
            return;
        }
        //if (cacheSlotIndex != m_QuickBar.m_ActiveSlot)
        {
            if (m_QuickBar.m_Slots[m_QuickBar.m_ActiveSlot].GetComponent<QuickBarSlot>().m_SlotUsed)
            {
                //m_HandObjects.UnEquip();
                m_ItemType = m_QuickBar.m_Slots[m_QuickBar.m_ActiveSlot].GetComponent<QuickBarSlot>().m_BarItemType;
                m_HandObjects.Equip(m_ItemType);
            }
            else
            {
                m_ItemType = Items.Empty;
                m_HandObjects.UnEquip();
            }
        }
        cacheSlotIndex = m_QuickBar.m_ActiveSlot;

    }
    public int GetTreeDamage()
    {
        switch (m_ItemType)
        {
            case Items.Pickaxe:
                return 3;
            case Items.Axe:
                return 5;
            default:
                return 2;
        }
    }

    public int GetRockDamage()
    {
        switch (m_ItemType)
        {
            case Items.Pickaxe:
                return 5;
            case Items.Axe:
                return 3;
            default:
                return 1;
        }
    }

    public int GetEnemyDamage()
    {
        switch (m_ItemType)
        {
            case Items.Pickaxe:
                return 20;
            case Items.Axe:
                return 20;
            default:
                return 10;
        }
    }
}
