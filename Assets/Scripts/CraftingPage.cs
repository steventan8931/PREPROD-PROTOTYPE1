using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPage : MonoBehaviour
{
    public GameObject[] m_Pages;
    public int m_PageIndex = 0;

    public void LastPage()
    {
        if (m_PageIndex > 0)
        {
            m_PageIndex--;
        }

        for (int i = 0; i < m_Pages.Length; i++)
        {
            m_Pages[i].SetActive(false);
        }

        m_Pages[m_PageIndex].SetActive(true);
    }

    public void NextPage()
    {
        if (m_PageIndex < m_Pages.Length - 1)
        {
            m_PageIndex++;
        }

        for (int i = 0; i < m_Pages.Length; i++)
        {
            m_Pages[i].SetActive(false);
        }

        m_Pages[m_PageIndex].SetActive(true);
    }
}
