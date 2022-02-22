using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int m_WoodCount = 0;
    public int m_RockCount = 0;

    public Text m_PressGText;

    private void Start()
    {
        m_PressGText.enabled = false;
    }

}
