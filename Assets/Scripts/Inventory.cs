using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Items
{
    Empty,
    Wood,
    Rock,
    Bedroll,
    Pickaxe,
    Axe,
    Fireplace,
    Tent,
    Chest,
    NPCHouse,
    Table1,
    Table2,
}

public class Inventory : MonoBehaviour
{
    public int m_WoodCount = 0;
    public int m_RockCount = 0;
    public int m_AxeCount = 0;
    public int m_PickaxeCount = 0;
    public int m_BedrollCount = 0;
    public int m_FireplaceCount = 0;
    public int m_ChestCount = 0;
    public int m_TentCount = 0;
    public int m_NPCHouseCount = 0;
    public int m_Table1Count = 0;
    public int m_Table2Count = 0;

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
    public GameObject m_TentUI;
    public GameObject m_ChestUI;
    public GameObject m_NPCHouseUI;
    public GameObject m_Table1UI;
    public GameObject m_Table2UI;

    public QuickBar m_QuickBar;

    public Quest m_playerQuest;

    public bool m_Unlocked = true;

    private CanvasManager canvas;

    public void ClearItems()
    {
        m_WoodCount = 0;
        m_RockCount = 0;
        //m_AxeCount = 0;
        //m_PickaxeCount = 0;
        //m_BedrollCount = 0;
        //m_FireplaceCount = 0;
        //m_ChestCount = 0;
        //m_TentCount = 0;
        //m_Table1Count = 0;
        //m_Table2Count = 0;
    }

    private void Awake()
    {
        m_QuickBar = FindObjectOfType<QuickBar>();
    }

    private void Start()
    {
        m_PressGText.enabled = false;
        m_Inventory.SetActive(true);
        ItemInInventory(m_WoodCount, m_WoodUI);
        ItemInInventory(m_RockCount, m_RockUI);
        m_Inventory.SetActive(false);
        //m_playerQuest = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>().CurrQuest;

        //For when we need to lock inventory
        m_Unlocked = false;

    }

    public void UnlockInventory()
    {
        m_Unlocked = true;
    }
    
    public void Prompt(string _prompt)
    {
        m_PressGText.text = _prompt;
        m_PressGText.enabled = true;
    }

    private void Update()
    {
        m_playerQuest = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>().CurrQuest;
        //Debug.Log("updating quest!");

        if (m_Unlocked)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (m_InventoryOpen)
                {
                    CanvasManager.Instance.Close();
                }
                m_InventoryOpen = !m_InventoryOpen;
            }
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
        //ItemInInventory(m_TentCount, m_TentUI);
        ItemInInventory(m_ChestCount, m_ChestUI);
        ItemInInventory(m_NPCHouseCount, m_NPCHouseUI);
        ItemInInventory(m_Table1Count, m_Table1UI);
        ItemInInventory(m_Table2Count, m_Table2UI);

        if (m_InventoryOpen)
        {
            if (CanvasManager.Instance.m_CanOpen)
            {
                m_Inventory.SetActive(true);
            }
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
            if (!m_InventoryOpen)
            {
                if (!_UIObject.GetComponent<ItemSlot>().m_Moved)
                {
                    //Debug.Log("moved");
                    _UIObject.GetComponent<ItemSlot>().AddToBar();
                }
            }
            else
            {
                if (m_QuickBar.InQuickBar(_UIObject.GetComponent<ItemSlot>().m_ItemType))
                {
                    _UIObject.SetActive(false);
                }
                else
                {
                    _UIObject.SetActive(true);
                }

            }
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
            case Items.Chest:
                m_ChestCount++;
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
            case Items.Tent:
                m_TentCount++;
                break;
            case Items.NPCHouse:
                m_NPCHouseCount++;
                break;
            case Items.Table1:
                m_Table1Count++;
                break;
            case Items.Table2:
                m_Table2Count++;
                break;
        }
    }

    public void AddItemToInventory(Items _ItemName, int _Count)
    {
        switch (_ItemName)
        {
            case Items.Wood:
                m_WoodCount+= _Count;
                break;
            case Items.Rock:
                m_RockCount += _Count;
                break;
            case Items.Chest:
                m_ChestCount += _Count; 
                break;
            case Items.Pickaxe:
                m_PickaxeCount += _Count;
                break;
            case Items.Axe:
                m_AxeCount += _Count;
                break;
            case Items.Bedroll:
                m_BedrollCount += _Count;
                break;
            case Items.Fireplace:
                m_FireplaceCount += _Count;
                break;
            case Items.Tent:
                m_TentCount += _Count;
                break;
            case Items.NPCHouse:
                m_NPCHouseCount += _Count;
                break;
            case Items.Table1:
                m_Table1Count+= _Count;
                break;
            case Items.Table2:
                m_Table2Count += _Count;
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
            case Items.Chest:
                m_ChestCount--;
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
            case Items.Tent:
                m_TentCount--;
                break;
            case Items.NPCHouse:
                m_NPCHouseCount--;
                break;
            case Items.Table1:
                m_Table1Count--;
                break;
            case Items.Table2:
                m_Table2Count--;
                break;
        }
    }

    public void RemoveItemFromInventory(Items _ItemName, int _Count)
    {
        switch (_ItemName)
        {
            case Items.Wood:
                m_WoodCount -= _Count;
                break;
            case Items.Rock:
                m_RockCount -= _Count; 
                break;
            case Items.Chest:
                m_ChestCount -= _Count; 
                break;
            case Items.Pickaxe:
                m_PickaxeCount -= _Count;
                break;
            case Items.Axe:
                m_AxeCount -= _Count;
                break;
            case Items.Bedroll:
                m_BedrollCount -= _Count;
                break;
            case Items.Fireplace:
                m_FireplaceCount -= _Count;
                break;
            case Items.Tent:
                m_TentCount -= _Count;
                break;
            case Items.NPCHouse:
                m_NPCHouseCount -= _Count;
                break;
            case Items.Table1:
                m_Table1Count -= _Count;
                break;
            case Items.Table2:
                m_Table2Count -= _Count;
                break;
        }
    }

    public GameObject GetItemUI(Items _ItemName)
    {
        switch (_ItemName)
        {
            case Items.Wood:
                return m_WoodUI;
            case Items.Rock:
                return m_RockUI;
            case Items.Chest:
                return m_ChestUI;

            case Items.Pickaxe:
                return m_PickaxeUI;
            case Items.Axe:
                return m_AxeUI;
            case Items.Bedroll:
                return m_BedrollUI;
            case Items.Fireplace:
                return m_FireplaceUI;
            case Items.Tent:
                return m_TentUI;
            case Items.NPCHouse:
                return m_NPCHouseUI;
            case Items.Table1:
                return m_Table1UI;
            case Items.Table2:
                return m_Table2UI;
        }
        return null;
    }

    public int GetItemCount(Items _ItemName)
    {
        switch (_ItemName)
        {
            case Items.Wood:
                return m_WoodCount;
            case Items.Rock:
                return m_RockCount;
            case Items.Chest:
                return m_ChestCount;
            case Items.Pickaxe:
                return m_PickaxeCount;
            case Items.Axe:
                return m_AxeCount;
            case Items.Bedroll:
                return m_BedrollCount;
            case Items.Fireplace:
                return m_FireplaceCount;
            case Items.Tent:
                return m_TentCount;
             case Items.NPCHouse:
                return m_NPCHouseCount;
            case Items.Table1:
                return m_Table1Count;
            case Items.Table2:
                return m_Table2Count;
        }
        return 0;
    }
}
