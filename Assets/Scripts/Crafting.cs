using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    [Header("UI + Prefabs Components")]
    public GameObject m_CraftingCanvas;


    private void Update()
    {
        //UpdateSlot(m_Inventory.m_WoodCount, m_SwordUI);

        if (Input.GetKeyDown(KeyCode.C))
        {
            m_CraftingCanvas.SetActive(!m_CraftingCanvas.activeInHierarchy);
        }
    }

}
