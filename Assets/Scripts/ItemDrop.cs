using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    Wood,
    Rock,
}

public class ItemDrop : MonoBehaviour
{
    public Items ItemType = new Items();
    private bool m_IsColliding = false;
    private Inventory cacheInventory;

    private void OnTriggerStay(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            m_IsColliding = true;
            cacheInventory = _other.GetComponent<Inventory>();

            cacheInventory.m_PressGText.enabled = true;
            //G to collect
            //if (Input.GetKeyDown(KeyCode.G))
            //{
            //    ChooseItem(cacheInventory);
            //    cacheInventory.m_PressGText.enabled = false;
            //    Destroy(gameObject);
            //}

        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            m_IsColliding = false;
            _other.GetComponent<Inventory>().m_PressGText.enabled = false;
        }
    }
    private void ChooseItem(Inventory _Inventory)
    {
        switch (ItemType)
        {
            case Items.Wood:
                _Inventory.m_WoodCount++;
                break;
            case Items.Rock:
                _Inventory.m_RockCount++;
                break;
        }
    }

    private void Update()
    {
        if (m_IsColliding)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                ChooseItem(cacheInventory);
                cacheInventory.m_PressGText.enabled = false;
                Destroy(gameObject);
            }
        }
    }
}



