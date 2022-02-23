using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [Header("Crafting Components")]
    public Inventory m_Inventory;
    public int m_Cost = 0;
    public bool m_EnoughMaterials = false;

    private void Start()
    {
        m_Inventory = FindObjectOfType<Inventory>();
    }


}
