using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItemController : MonoBehaviour
{
    public GameObject m_RockPrefab;
    public GameObject m_WoodPrefab;
    public GameObject m_BedrollPrefab;
    public GameObject m_FireplacePrefab;
    public GameObject m_TentPrefab;

    public Inventory m_Inventory;
    public Crafting m_Crafting;

    private void Start()
    {
        m_Inventory = FindObjectOfType<Inventory>();
        m_Crafting = FindObjectOfType<Crafting>();
    }

    public bool CanBuild()
    {
        if (m_Inventory.m_InventoryOpen || m_Crafting.m_CraftingOpen)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //Get Item for Building
    public GameObject GetItem(Items _ItemType)
    {
        switch (_ItemType)
        {
            case Items.Empty:
                return null;
            case Items.Rock:
                return m_RockPrefab;
            case Items.Wood:
                return m_WoodPrefab;
            case Items.Sword:
                return m_WoodPrefab;
            case Items.Bedroll:
                return m_BedrollPrefab;
            case Items.Axe:
                return null;
            case Items.Pickaxe:
                return null;
            case Items.Fireplace:
                return m_FireplacePrefab;
            case Items.Tent:
                return m_TentPrefab;
        }

        return null;
    }

    //Remove item from inventory after usage
    public void UseItem(Items _ItemType)
    {
        m_Inventory.RemoveItemFromInventory(_ItemType);
    }
}
