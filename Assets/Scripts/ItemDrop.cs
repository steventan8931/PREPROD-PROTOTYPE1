using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public Items ItemType = new Items();
    private bool m_IsColliding = false;
    private Inventory cacheInventory;
    private CharacterMotor m_Player;

    private void OnTriggerStay(Collider _other)
    {
        if (_other.GetComponent<Inventory>() != null)
        {
            m_IsColliding = true;
            cacheInventory = _other.GetComponent<Inventory>();
            m_Player = _other.GetComponent<CharacterMotor>();

            cacheInventory.m_PressGText.enabled = true;
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
        _Inventory.AddItemToInventory(ItemType);
        //switch (ItemType)
        //{
        //    case Items.Wood:
        //        _Inventory.m_WoodCount++;
        //        break;
        //    case Items.Rock:
        //        _Inventory.m_RockCount++;
        //        break;
        //}
    }

    private void Update()
    {
        if (m_IsColliding)
        {
            cacheInventory.Prompt("Press F to Gather");
            if (Input.GetKeyDown(KeyCode.F))
            {
                ChooseItem(cacheInventory);
                cacheInventory.m_PressGText.enabled = false;
                m_Player.m_Animation.ResetTrigger("Attacking");
                m_Player.m_Animation.SetTrigger("Attacking");
                Destroy(gameObject);
            }
        }
    }
}



