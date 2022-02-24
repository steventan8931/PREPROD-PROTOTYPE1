using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickBar : MonoBehaviour
{
    public GameObject[] m_Slots;
    public int m_ActiveSlot = 0;

    private void Start()
    {
        ShowSelectedSlot();
    }

    private void Update()
    {


        if (-Input.mouseScrollDelta.y > 0)
        {
            m_ActiveSlot++;
            ShowSelectedSlot();
        }
        if (-Input.mouseScrollDelta.y < 0)
        {
            m_ActiveSlot--;
            ShowSelectedSlot();
        }
    }

    private void CheckValid()
    {
        if (m_ActiveSlot >= m_Slots.Length)
        {
            m_ActiveSlot = m_Slots.Length - 1;
        }

        if (m_ActiveSlot <= 0)
        {
            m_ActiveSlot = 0;
        }
    }

    private void ShowSelectedSlot()
    {
        CheckValid();

        for (int i = 0; i < m_Slots.Length; i++)
        {
            m_Slots[i].GetComponent<Image>().color = Color.white;
        }
        m_Slots[m_ActiveSlot].GetComponent<Image>().color = Color.red;

    }
}
