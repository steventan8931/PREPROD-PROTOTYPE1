using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public GameObject m_Inventory;
    public GameObject m_Crafting;
    public GameObject m_Chest;

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
    }

    private void Update()
    {
        if (m_Inventory.activeInHierarchy || m_Chest.activeInHierarchy || m_Crafting.activeInHierarchy)
        {
            m_CanOpen = false;
        }
        else
        {
            m_CanOpen = true;
        }
    }
}
