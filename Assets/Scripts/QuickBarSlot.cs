using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickBarSlot : MonoBehaviour
{
    public bool m_SlotUsed = false;
    public Items m_BarItemType;

    private void Update()
    {

    }

    public void UseSlot(Items _ItemType, Image _Image)
    {
        m_BarItemType = _ItemType;
        Instantiate(_Image,transform);
        m_SlotUsed = true;
    }

    public void RemoveSlot()
    {
        m_SlotUsed = false;
        m_BarItemType = Items.Empty;
        Destroy(transform.GetChild(0).gameObject);
    }
}
