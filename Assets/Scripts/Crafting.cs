using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    [Header("UI + Prefabs Components")]
    public GameObject m_CraftingCanvas;
    public bool m_CraftingOpen = false;
    public bool m_Unlocked = true;


    private CanvasManager canvas;

    private void Start()
    {
        // m_Unlocked = false;        
    }

    public void UnlockCrafting()
    {
        m_Unlocked = true;
    }

    private void Update()
    {
        if (m_Unlocked)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                m_CraftingOpen = !m_CraftingOpen;

                if (m_CraftingOpen)
                {
                    if (CanvasManager.Instance.m_CanOpen)
                    {
                        m_CraftingCanvas.SetActive(true);
                    }
                }
                else
                {
                    m_CraftingCanvas.SetActive(false);
                }

            }
        }

    }

}
