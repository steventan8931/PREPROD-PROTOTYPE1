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
    Bedroll,
    Pickaxe,
    Axe,
}

public class Inventory : MonoBehaviour
{
    public int m_WoodCount = 0;
    public int m_RockCount = 0;
    public int m_BedrollCount = 0;
    public int m_AxeCount = 0;
    public int m_PickaxeCount = 0;
    public int m_Sword;

    public Text m_PressGText;
    public GameObject m_Inventory;
    public bool m_InventoryOpen = false;
    //public ItemSlot[] m_ItemSlots;

    //public List<bool> m_ItemInInventory;
    //public Items ItemType = new Items();

    public GameObject m_WoodUI;
    public GameObject m_RockUI;
    public GameObject m_AxeUI;
    public GameObject m_PickaxeUI;

    private QuickBar m_QuickBar;

    private void Start()
    {
        m_QuickBar = FindObjectOfType<QuickBar>();
        m_PressGText.enabled = false;
        m_Inventory.SetActive(true);
        ItemInInventory(m_WoodCount, m_WoodUI);
        ItemInInventory(m_RockCount, m_RockUI);
        m_Inventory.SetActive(false);

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
        ItemInInventory(m_AxeCount, m_AxeUI);
        ItemInInventory(m_PickaxeCount, m_PickaxeUI);
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
            _UIObject.SetActive(false);
        }
    }

}
