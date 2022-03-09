using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
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

    public Inventory m_Inventory;

    public List<ChestSlot> m_Slots;

    public GameObject m_ChestCanvas;

    public GameObject m_WoodUI;
    public GameObject m_RockUI;
    public GameObject m_AxeUI;
    public GameObject m_PickaxeUI;
    public GameObject m_BedrollUI;
    public GameObject m_FireplaceUI;
    public GameObject m_TentUI;
    public GameObject m_ChestUI;
    public GameObject m_NPCHouseUI;

    public void Assign()
    {
        m_Slots.Clear();
        for (int i = 0; i < m_ChestCanvas.transform.GetChild(2).transform.childCount; i++)
        {
            m_Slots.Add(m_ChestCanvas.transform.GetChild(2).GetChild(i).GetComponent<ChestSlot>());
        }

        foreach (ChestSlot slot in m_Slots)
        {
            slot.m_Chest = GetComponent<Chest>();
            SetItemUI(slot.m_ItemType,  slot.gameObject);
        }

    }

    public void SetItemUI(Items _ItemName, GameObject _Object)
    {
        switch (_ItemName)
        {
            case Items.Wood:
                m_WoodUI = _Object;
                break;
            case Items.Rock:
                m_RockUI = _Object; 
                break;
            case Items.Chest:
                m_ChestUI = _Object;
                break;
            case Items.Pickaxe:
                m_PickaxeUI = _Object;
                break;
            case Items.Axe:
                m_AxeUI = _Object;
                break;
            case Items.Bedroll:
                m_BedrollUI = _Object;
                break;
            case Items.Fireplace:
                m_FireplaceUI = _Object;
                break;
            case Items.Tent:
                m_TentUI = _Object;
                break;
            case Items.NPCHouse:
                m_NPCHouseUI = _Object;
                break;
        }
    }
    public void ManagedUpdate()
    {
        ItemInInventory(m_WoodCount, m_WoodUI);
        ItemInInventory(m_RockCount, m_RockUI);
        ItemInInventory(m_AxeCount, m_AxeUI);
        ItemInInventory(m_PickaxeCount, m_PickaxeUI);
        ItemInInventory(m_FireplaceCount, m_FireplaceUI);
        ItemInInventory(m_BedrollCount, m_BedrollUI);
        ItemInInventory(m_TentCount, m_TentUI);
        ItemInInventory(m_ChestCount, m_ChestUI);
        ItemInInventory(m_NPCHouseCount, m_NPCHouseUI);
    }

    private void ItemInInventory(int _ItemCount, GameObject _UIObject)
    {
        if (_UIObject == null)
        {
            return;
        }

        if (_ItemCount >= 1)
        {

            _UIObject.SetActive(true);
            _UIObject.GetComponent<ChestSlot>().EnableSlot(_ItemCount);
        }
        else
        {
            //_UIObject.GetComponent<ItemSlot>().m_RemoveFromBar = true;
            _UIObject.SetActive(false);
        }
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
        }
        return 0;
    }

    public void RemoveItemFromChest(Items _ItemName, int _Count)
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
        }
    }
}
