﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public bool m_IsSlotUsed = false;
    private Image m_Image;
    private Text m_Count;

    private void Start()
    {
        m_Image = GetComponentInChildren<Image>();
        m_Count = GetComponentInChildren<Text>();

        m_Image.enabled = false;
        m_Count.enabled = false;
    }

    public void EnableSlot(int _Count)
    {
        m_Image.enabled = true;
        m_Count.enabled = true;

        m_Count.text = _Count.ToString();
    }

    private void Update()
    {

    }
}
