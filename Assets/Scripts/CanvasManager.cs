using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public GameObject m_Inventory;
    public GameObject m_Crafting;
    public GameObject m_Chest;

    public Crafting m_Craft;
    public Inventory m_Inven;
    public bool m_CanOpen = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        m_Inven = FindObjectOfType<Inventory>();
        m_Craft = FindObjectOfType<Crafting>();
    }

    public void Close()
    {

    }
    private void Update()
    {
        CheckValid();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_Inventory.SetActive(false);
            m_Crafting.SetActive(false);
            m_Chest.SetActive(false);
            m_CanOpen = true;
            m_Inven.m_InventoryOpen = false;
            m_Craft.m_CraftingOpen = false;
        }

        if (!m_CanOpen)
        {
            m_Inven.m_InventoryOpen = false;
            m_Craft.m_CraftingOpen = false;
        }
    }

    private void CheckValid()
    {
        if (!m_Craft.m_CraftingOpen)
        {
            m_CanOpen = true;
        }
        else
        {
            m_CanOpen = false;
            m_Inven.m_InventoryOpen = false;
        }

        if (!m_Inven.m_InventoryOpen)
        {
            m_CanOpen = true;
        }
        else
        {
            m_CanOpen = false;
            m_Craft.m_CraftingOpen = false;
        }



        if (m_Chest.activeInHierarchy)
        {
            m_CanOpen = false;
            return;
        }
        else
        {
            m_CanOpen = true;
        }
    }
}
