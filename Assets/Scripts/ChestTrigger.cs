using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public Animator m_Animation;
    private CharacterMotor m_Player;
    public bool m_IsColliding = false;
    public bool m_ChestOpen = false;

    private Chest m_Chest;
    private AudioManager m_Audio;
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
            m_Chest.m_Inventory = m_Player.GetComponent<Inventory>();
            m_IsColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterMotor>())
        {
            m_IsColliding = false;
        }
    }

    private void Update()
    {
        if (m_IsColliding)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Chest.m_ChestCanvas.SetActive(!m_ChestOpen);
                m_Chest.Assign();
                AudioManager.Instance.PlayAudio("chestopen");
                m_ChestOpen = !m_ChestOpen;
            }
        }

        if (m_ChestOpen)
        {
            m_Chest.ManagedUpdate();
            m_Animation.SetBool("open", true);
        }
        else
        {
            m_Animation.SetBool("open", false);
        }
    }
}
