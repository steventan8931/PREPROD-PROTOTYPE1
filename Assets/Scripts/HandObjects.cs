using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandObjects : MonoBehaviour
{
    public GameObject[] m_Object;

    public int m_WeaponIndex = 99;

    public void Equip(Items _ItemType)
    {
        UnEquip();
        for (int i = 0; i < m_Object.Length; i++)
        {
            if (m_Object[i].GetComponent<HandItem>().m_ItemType == _ItemType)
            {
                m_WeaponIndex = i;
            }
        }

        if (m_WeaponIndex != 99)
        {
            m_Object[m_WeaponIndex].SetActive(true);
        }

    }

    public void UnEquip()
    {
        for (int i = 0; i < m_Object.Length; i++)
        {
            m_Object[i].SetActive(false);
        }
        m_WeaponIndex = 99;
    }
}
