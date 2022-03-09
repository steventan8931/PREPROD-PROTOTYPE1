using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickBarSlot : MonoBehaviour
{
    public bool m_SlotUsed = false;
    public Items m_BarItemType;
    public GameObject m_LinkedItemSlot;
    private Inventory m_Inventory;

    private void Start()
    {
        m_Inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (m_LinkedItemSlot)
        {
            if (m_LinkedItemSlot.GetComponent<ItemSlot>())
            {
                if (m_LinkedItemSlot.GetComponent<ItemSlot>().m_RemoveFromBar)
                {
                    m_LinkedItemSlot = null;
                    RemoveSlot();
                }
            }
        }
    }

    public void UseSlot(Items _ItemType, Image _Image, GameObject _LinkedItem)
    {
        m_BarItemType = _ItemType;
        m_LinkedItemSlot = _LinkedItem;
        Instantiate(_Image,transform);
        m_SlotUsed = true;
    }

    public void RemoveSlot()
    {
        m_SlotUsed = false;
        m_BarItemType = Items.Empty;
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
