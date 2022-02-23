using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    [Header("Crafting Components")]
    public Inventory m_Inventory;
    public int m_Cost = 0;
    public bool m_EnoughMaterials = false;
    string m_ItemName;

    [Header("UI + Prefabs Components")]
    public GameObject m_CraftingCanvas;
    public GameObject m_SwordUI;

    private void Start()
    {
        m_Inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        UpdateSlot(m_Inventory.m_WoodCount, m_SwordUI);

        if (Input.GetKeyDown(KeyCode.C))
        {
            m_CraftingCanvas.SetActive(!m_CraftingCanvas.activeInHierarchy);
        }
    }

    private void UpdateSlot(int _ItemCost, GameObject _CraftUI)
    {
        if (_ItemCost >= m_Cost)
        {
            _CraftUI.GetComponent<Button>().interactable = true;
        }
        else
        {
            _CraftUI.GetComponent<Button>().interactable = false;
        }
    }

    //Button Functions
    public void AddItemName(string _ItemName)
    {
        m_ItemName = _ItemName;
    }

    public void AddItem()
    {
        if (m_EnoughMaterials)
        {
            switch (m_ItemName)
            {
                case "Sword":
                    //m_Inventory.m_FenceBlockCount++;
                    m_Inventory.m_WoodCount -= m_Cost;
                    m_Inventory.m_Sword++;
                    Debug.Log("added sword");
                    m_EnoughMaterials = false;
                    break;
            }
        }
    }

    public void SetItemCostCount(int _ItemCostCount)
    {
        m_Cost = _ItemCostCount;
        m_EnoughMaterials = false;
    }

    public void SetItemCost(string _ItemName)
    {
        switch (_ItemName)
        {
            //wooden sword
            case "Sword":
                if (m_Inventory.m_WoodCount >= m_Cost)
                {
                    m_ItemName = _ItemName;
                    m_EnoughMaterials = true;
                }
                break;
            case "Rock":
                if (m_Inventory.m_RockCount >= m_Cost)
                {
                    m_ItemName = _ItemName;
                    m_EnoughMaterials = true;
                }
                break;
        }
    }
}
