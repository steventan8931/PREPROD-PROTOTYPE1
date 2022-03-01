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
    Fireplace,
}

public class Inventory : MonoBehaviour
{
    public int m_WoodCount = 0;
    public int m_RockCount = 0;
    public int m_AxeCount = 0;
    public int m_PickaxeCount = 0;
    public int m_BedrollCount = 0;
    public int m_FireplaceCount;
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
    public GameObject m_BedrollUI;
    public GameObject m_FireplaceUI;

    private QuickBar m_QuickBar;

    public Quest m_playerQuest;
    private void Start()
    {
        m_QuickBar = FindObjectOfType<QuickBar>();
        m_PressGText.enabled = false;
        m_Inventory.SetActive(true);
        ItemInInventory(m_WoodCount, m_WoodUI);
        ItemInInventory(m_RockCount, m_RockUI);
        m_Inventory.SetActive(false);
        m_playerQuest = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>().CurrQuest;
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
        ItemInInventory(m_FireplaceCount, m_FireplaceUI);
        ItemInInventory(m_BedrollCount, m_BedrollUI);
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

    public void AddItemToInventory(Items _ItemName)
    {
        switch (_ItemName)
        {
            case Items.Wood:
                m_WoodCount++;
                m_playerQuest.goal.WoodGathered();
                break;
            case Items.Rock:
                m_RockCount++;
                m_playerQuest.goal.RockGathered();
                break;
            case Items.Sword:
                m_Sword++;
                break;
            case Items.Pickaxe:
                m_PickaxeCount++;
                break;
            case Items.Axe:
                m_AxeCount++;
                break;
            case Items.Bedroll:
                m_BedrollCount++;
                break;
            case Items.Fireplace:
                m_FireplaceCount++;
                break;
        }
    }

    public void RemoveItemFromInventory(Items _ItemName)
    {
        switch (_ItemName)
        {
            case Items.Wood:
                m_WoodCount--;
                break;
            case Items.Rock:
                m_RockCount--;
                break;
            case Items.Sword:
                m_Sword--;
                break;
            case Items.Pickaxe:
                m_PickaxeCount--;
                break;
            case Items.Axe:
                m_AxeCount--;
                break;
            case Items.Bedroll:
                m_BedrollCount--;
                break;
            case Items.Fireplace:
                m_FireplaceCount--;
                break;
        }
    }

}
