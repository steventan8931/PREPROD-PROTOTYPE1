using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TentTrigger : MonoBehaviour
{
    public Animator m_Animation;
    private CharacterMotor m_Player;
    public bool m_IsColliding = false;
    public bool m_TentOpen = false;

    private void Start()
    {
        //m_Animation = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterMotor>())
        {
            m_Player = other.GetComponent<CharacterMotor>();
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
        if (m_IsColliding && m_Player.m_FinishedQuests)
        {
            m_Player.GetComponent<Inventory>().Prompt("Press F to Enter The Tent");
            Debug.Log("in tent");
            if (Input.GetKeyDown(KeyCode.F))
            {
                m_Player.m_Animation.ResetTrigger("Attacking");
                m_Player.m_Animation.SetTrigger("Attacking");
                m_TentOpen = !m_TentOpen;
            }
        }

        if (m_TentOpen)
        {
            m_Animation.SetBool("open", true);
            SceneManager.LoadScene(2);
        }
        else
        {
            m_Animation.SetBool("open", false);
        }
    }
}
