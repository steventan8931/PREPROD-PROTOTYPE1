using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    [Header("UI + Prefabs Components")]
    public GameObject m_CraftingCanvas;
    public bool m_CraftingOpen = false;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            m_CraftingCanvas.SetActive(!m_CraftingCanvas.activeInHierarchy);
            m_CraftingOpen = m_CraftingCanvas.activeInHierarchy;
        }
    }

}
