using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ItemCosts
{
    public Items ItemType;
    public int ItemCost;
}

public class CraftingSlot : MonoBehaviour
{
    public List<ItemCosts> m_CraftMaterials;
    public Items m_CraftingOutcome;
    public bool m_EnoughMaterials = false;
    private Inventory m_Inventory;

    private void Start()
    {
        m_Inventory = FindObjectOfType<Inventory>();
        Debug.Log("start size  = " + m_CraftMaterials.Count);
    }

    private void Update()
    {
        CheckTotalCost();
        if (m_EnoughMaterials)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void CraftItem()
    {
        m_Inventory.AddItemToInventory(m_CraftingOutcome);
        RemoveItemsFromInventory();
    }

    private void RemoveItemsFromInventory()
    {
        for (int i = 0; i < m_CraftMaterials.Count; i++)
        {
            RemoveItem(m_CraftMaterials[i]);
        }
    }

    private void RemoveItem(ItemCosts _Cost)
    {
        switch (_Cost.ItemType)
        {
            case Items.Wood:
                m_Inventory.m_WoodCount -= _Cost.ItemCost;
                break;
            case Items.Rock:
                m_Inventory.m_RockCount -= _Cost.ItemCost;
                break;
        }
    }

    private void CheckTotalCost()
    {
        foreach (ItemCosts item in m_CraftMaterials)
        {
            if (!CheckItemCost(item))
            {
                m_EnoughMaterials = false;
                break;
            }
        }

    }

    private bool CheckItemCost(ItemCosts _Cost)
    {
        switch (_Cost.ItemType)
        {
            case Items.Wood:
                if (m_Inventory.m_WoodCount >= _Cost.ItemCost)
                {
                    m_EnoughMaterials = true;
                    return true;
                }
                break;
            case Items.Rock:
                if (m_Inventory.m_RockCount >= _Cost.ItemCost)
                {
                    m_EnoughMaterials = true;
                    return true;
                }
                break;
        }

        return false;
    }

}
