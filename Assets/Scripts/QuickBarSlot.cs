using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickBarSlot : MonoBehaviour
{
    public bool m_SlotUsed = false;
    public Items m_BarItemType;
    public GameObject m_LinkedItemSlot;
    private Inventory m_Inventory;

    public ChestTrigger m_ChestTrigger;
    private void Start()
    {
        m_Inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (m_LinkedItemSlot)
        {
            if (m_LinkedItemSlot.GetComponent<ItemSlot>())
            {
                if (transform.childCount > 1)
                {
                    transform.GetChild(1).GetComponent<Text>().text = m_Inventory.GetItemCount(m_LinkedItemSlot.GetComponent<ItemSlot>().m_ItemType).ToString();
                }
                if (m_LinkedItemSlot.GetComponent<ItemSlot>().m_RemoveFromBar)
                {
                    m_LinkedItemSlot = null;
                    RemoveSlot();
                }
            }
        }
    }

    public void UseSlot(Items _ItemType, Image _Image, GameObject _LinkedItem)
    {
        m_BarItemType = _ItemType;
        m_LinkedItemSlot = _LinkedItem;
        Instantiate(_Image, transform);
        //GameObject obj = new GameObject();
        //obj.AddComponent<Text>();
        //Instantiate(obj, transform);
        Instantiate(_LinkedItem.transform.GetChild(1), transform);
        m_SlotUsed = true;
        if (m_Inventory.GetItemCount(_ItemType) > 0 && m_LinkedItemSlot.GetComponent<ItemSlot>().m_Moved)
        {
            RemoveSlot();
        }
    }

    public void RemoveSlot()
    {
        if (m_ChestTrigger != null)
        {
            Debug.Log("yes");

            if (m_ChestTrigger.m_ChestOpen)
            {
                if (m_LinkedItemSlot)
                {
                    Debug.Log("yes");
                    m_ChestTrigger.m_Chest.AddItemToChest(m_LinkedItemSlot.GetComponent<ItemSlot>().m_ItemType, m_Inventory.GetItemCount(m_LinkedItemSlot.GetComponent<ItemSlot>().m_ItemType));
                    m_Inventory.RemoveItemFromInventory(m_LinkedItemSlot.GetComponent<ItemSlot>().m_ItemType, m_Inventory.GetItemCount(m_LinkedItemSlot.GetComponent<ItemSlot>().m_ItemType));
                    m_SlotUsed = false;
                    m_BarItemType = Items.Empty;
                    if (m_LinkedItemSlot)
                    {
                        m_LinkedItemSlot.GetComponent<ItemSlot>().m_Moved = true;
                    }
                    if (transform.childCount > 0)
                    {
                        Destroy(transform.GetChild(0).gameObject);
                        Destroy(transform.GetChild(1).gameObject);

                    }
                }
            }
            else if (!m_Inventory.m_InventoryOpen)
            {
                return;
            }
        }

        if (!m_Inventory.m_InventoryOpen)
        {
            return;
        }
        m_SlotUsed = false;
        m_BarItemType = Items.Empty;
        if (m_LinkedItemSlot)
        {
            m_LinkedItemSlot.GetComponent<ItemSlot>().m_Moved = true;
        }
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            Destroy(transform.GetChild(1).gameObject);

        }
    }
}
