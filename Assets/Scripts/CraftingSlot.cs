﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    public Items[] m_CraftingMaterials;
    public int[] m_MaterialCost;
    public Items m_CraftingOutcome;
    public bool m_EnoughMaterials = false;
    private Inventory m_Inventory;

    private void Start()
    {
        m_Inventory = FindObjectOfType<Inventory>();
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
        AddItemToInventory(m_CraftingOutcome);
        RemoveItemsFromInventory();
    }

    private void AddItemToInventory(Items _ItemName)
    {
        switch (_ItemName)
        {
            case Items.Wood:
                m_Inventory.m_WoodCount++;
                break;
            case Items.Rock:
                m_Inventory.m_RockCount++;
                break;
            case Items.Sword:
                m_Inventory.m_Sword++;
                break;
        }
    }
    private void RemoveItemsFromInventory()
    {
        for (int i = 0; i < m_CraftingMaterials.Length; i++)
        {
            RemoveItem(m_CraftingMaterials[i], m_MaterialCost[i]);
        }
    }

    private void RemoveItem(Items _ItemName, int _CostIndex)
    {
        switch (_ItemName)
        {
            case Items.Wood:
                m_Inventory.m_WoodCount -= m_MaterialCost[_CostIndex - 1];
                break;
            case Items.Rock:
                m_Inventory.m_RockCount -= m_MaterialCost[_CostIndex - 1];
                break;
        }
    }

    private void CheckTotalCost()
    {
       for (int i = 0; i < m_CraftingMaterials.Length; i++)
        {
            if (!CheckItemCost(m_CraftingMaterials[i], m_MaterialCost[i]))
            {
                m_EnoughMaterials = false;
                break;
            }
        }
    }

    private bool CheckItemCost(Items _ItemName, int _CostIndex)
    {
        switch (_ItemName)
        {
            case Items.Wood:
                if (m_Inventory.m_WoodCount >= m_MaterialCost[_CostIndex-1])
                {
                    m_EnoughMaterials = true;
                    return true;
                }
                break;
            case Items.Rock:
                if (m_Inventory.m_RockCount >= m_MaterialCost[_CostIndex-1])
                {
                    m_EnoughMaterials = true;
                    return true;
                }
                break;
        }

        return false;
    }

}