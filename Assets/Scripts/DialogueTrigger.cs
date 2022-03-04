using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] m_Dialogue;
    public DialogueManager m_DialogueManager;
    public bool m_FirstTimeDialogue = true;
    private bool m_Colliding = false;
    private CharacterMotor m_Player;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<CharacterMotor>())
        {
            if (!m_Player)
            {
                m_Player = _other.GetComponent<CharacterMotor>();
            }
            m_Colliding = true;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.GetComponent<CharacterMotor>())
        {
            m_Colliding = false;
        }
    }

    private void Update()
    {
        if (m_Colliding)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (m_FirstTimeDialogue)
                {
                    TriggerFirstTimeDialogue();
                    m_FirstTimeDialogue = false;
                }
                else
                {
                    TriggerRepeatDialogue();
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                DialogueManager.Instance.DisplayNextSentence();
            }

        }
        else
        {


        }

    }
    public void TriggerFirstTimeDialogue()
    {
        DialogueManager.Instance.StartDialogue(m_Dialogue[0]);
    }

    public void TriggerRepeatDialogue()
    {
        DialogueManager.Instance.StartDialogue(m_Dialogue[1]);
    }
}
