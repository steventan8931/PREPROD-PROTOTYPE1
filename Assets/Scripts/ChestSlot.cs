using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{
    public bool m_IsSlotUsed = false;
    public Image m_Image;
    public Text m_Count;
    public Items m_ItemType;

    private QuickBar m_Bar;
    public bool m_RemoveFromBar = false;
    private bool cacheCanAddTobar = true;
    public Inventory m_Inventory;

    public Chest m_Chest;

    private void Awake()
    {
        m_Inventory = FindObjectOfType<Inventory>();
        m_Image = GetComponentInChildren<Image>();
        m_Count = GetComponentInChildren<Text>();
        m_Bar = FindObjectOfType<QuickBar>();
    }

    public void AddToInventory()
    {
        cacheCanAddTobar = true;
        for (int i = 0; i < m_Bar.m_Slots.Length; i++)
        {
            //Slot is not used and item type doesnt exist already
            if (!m_Bar.m_Slots[i].GetComponent<QuickBarSlot>().m_SlotUsed)
            {
                //Check if already in a slot
                for (int j = 0; j < m_Bar.m_Slots.Length; j++)
                {
                    if (m_Bar.m_Slots[j].GetComponent<QuickBarSlot>().m_BarItemType == m_ItemType)
                    {
                        //cacheCanAddTobar = false;
                        break;
                    }
                }

                if (cacheCanAddTobar)
                {
                    m_Bar.m_Slots[i].GetComponent<QuickBarSlot>().UseSlot(m_ItemType, m_Image, m_Chest.m_Inventory.GetItemUI(m_ItemType));
                    m_Inventory.AddItemToInventory(m_ItemType,m_Chest.GetItemCount(m_ItemType));
                    m_Chest.RemoveItemFromChest(m_ItemType, m_Chest.GetItemCount(m_ItemType));
                    break;
                }
            }
        }
    }

    public void EnableSlot(int _Count)
    {
        m_Count.text = _Count.ToString();
    }
}
