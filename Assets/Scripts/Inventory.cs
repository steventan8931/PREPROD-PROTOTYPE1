using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int m_WoodCount = 0;
    public int m_RockCount = 0;

    public Text m_PressGText;
    public GameObject m_Inventory;
    public bool m_InventoryOpen = false;

    private void Start()
    {
        m_PressGText.enabled = false;
        UpdateInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            m_InventoryOpen = !m_InventoryOpen;
            UpdateInventory();
        }
    }

    private void UpdateInventory()
    {
        if (m_InventoryOpen)
        {
            m_Inventory.SetActive(true);
        }
        else
        {
            m_Inventory.SetActive(false);
        }
    }
}
