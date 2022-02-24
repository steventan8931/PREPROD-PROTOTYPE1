using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveInHand
{
    Hand,
    Sword,
    Axe,
}

public class Hand : MonoBehaviour
{
    public Inventory m_Inventory;
    public ActiveInHand m_Hand;

    private void Start()
    {
        m_Inventory = GetComponent<Inventory>();
    }

    private void Update()
    {

        if (m_Hand < ActiveInHand.Hand)
        {
            m_Hand = ActiveInHand.Sword;
        }
        if (m_Hand > ActiveInHand.Axe)
        {
            m_Hand = ActiveInHand.Hand;
        }

    }
}
