using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public Animator m_Animation;
    private CharacterMotor m_Player;
    public bool m_IsColliding = false;
    public bool m_ChestOpen = false;

    public Chest m_Chest;
    private AudioManager m_Audio;

    private CanvasManager canvas;
    
    private void Start()
    {
        //m_Animation = GetComponent<Animator>();
        m_Chest = GetComponent<Chest>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterMotor>())
        {
            m_Player = other.GetComponent<CharacterMotor>();
            m_Player.GetComponent<Inventory>().m_QuickBar.cacheChest = this;
            m_Chest.m_Inventory = m_Player.GetComponent<Inventory>();
            m_IsColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterMotor>())
        {
            m_IsColliding = false;
            m_Player.GetComponent<Inventory>().m_PressGText.enabled = false;
        }
    }

    private void Update()
    {
        if (m_IsColliding)
        {
            m_Player.GetComponent<Inventory>().Prompt("Press F to Open");
            if (Input.GetKeyDown(KeyCode.F))
            {
                m_Chest.Assign();
                AudioManager.Instance.PlayAudio("chestopen");
                m_ChestOpen = !m_ChestOpen;
                if (!m_ChestOpen)
                {
                    m_Chest.m_ChestCanvas.SetActive(false);
                    m_Player.GetComponent<Inventory>().m_PressGText.enabled = false;
                }

            }
        }


        if (m_ChestOpen)
        {
            if (CanvasManager.Instance.m_CanOpen)
            {
                m_Chest.m_ChestCanvas.SetActive(true);
                m_Player.GetComponent<Inventory>().m_PressGText.enabled = false;
            }
            m_Chest.ManagedUpdate();
            m_Player.m_CanMove = false;
            m_Animation.SetBool("open", true);
        }
        else
        {
            if (m_Player)
            {
                m_Player.m_CanMove = true;
            }
            m_Animation.SetBool("open", false);
        }
    }
}
