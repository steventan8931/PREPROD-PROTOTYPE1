using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Items
{
    Empty,
    Wood,
    Rock,
    Sword,
}

public class Inventory : MonoBehaviour
{
    public int m_WoodCount = 0;
    public int m_RockCount = 0;
    public int m_Sword;

    public Text m_PressGText;
    public GameObject m_Inventory;
    public bool m_InventoryOpen = false;
    //public ItemSlot[] m_ItemSlots;

    //public List<bool> m_ItemInInventory;
    //public Items ItemType = new Items();

    public GameObject m_WoodUI;
    public GameObject m_RockUI;

    private QuickBar m_QuickBar;

    private void Start()
    {
        m_QuickBar = FindObjectOfType<QuickBar>();
        m_PressGText.enabled = false;
        m_Inventory.SetActive(true);
        ItemInInventory(m_WoodCount, m_WoodUI);
        ItemInInventory(m_RockCount, m_RockUI);
        m_Inventory.SetActive(false);
        //for (int i = 0; i < System.Enum.GetValues(typeof(Items)).Length; i++)
        //{
        //    m_ItemInInventory.Add(false);
        //}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            m_InventoryOpen = !m_InventoryOpen;
        }
        UpdateInventory();

    }

    private void UpdateInventory()
    {
        ItemInInventory(m_WoodCount, m_WoodUI);
        ItemInInventory(m_RockCount, m_RockUI);
        if (m_InventoryOpen)
        {
            m_Inventory.SetActive(true);
        }
        else
        {
            m_Inventory.SetActive(false);
        }
    }

    private void ItemInInventory(int _ItemCount, GameObject _UIObject)
    {
        if (_ItemCount >= 1)
        {
            _UIObject.SetActive(true);
            _UIObject.GetComponent<ItemSlot>().EnableSlot(_ItemCount);
        }
        else
        {
            _UIObject.GetComponent<ItemSlot>().m_RemoveFromBar = true;
            //for (int i = 0; i < m_QuickBar.m_Slots.Length;i++)
            //{
            //    if (m_QuickBar.m_Slots[i].GetComponent<QuickBarSlot>().m_LinkedItemSlot.GetInstanceID() == _UIObject.GetInstanceID())
            //    {
            //        m_QuickBar.m_Slots[i].GetComponent<QuickBarSlot>().RemoveSlot();
            //    }
            //}

            _UIObject.SetActive(false);
        }
    }

}
