using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] m_Dialogue;
    public DialogueManager m_DialogueManager;
    public bool m_QuestOneDialogue = true;
    public bool m_QuestOneCompleted = false;
    public bool m_QuestTwoDialogue = true;
    public bool m_QuestTwoCompleted = false;
    private bool m_Colliding = false;
    private CharacterMotor m_Player;
    public bool m_IsTalking = false;
    public Sprite m_Sprite;

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
            m_Player.GetComponent<Inventory>().m_PressGText.enabled = false;
        }
    }

    private void Update()
    {
        if (m_Colliding)
        {
            if (!m_IsTalking)
            {
                m_Player.GetComponent<Inventory>().Prompt("Press F to Talk to the NPC");
            }
            else
            {
                m_Player.GetComponent<Inventory>().m_PressGText.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                //If quest 1 hasnt been completed
                if (m_QuestOneDialogue || !m_QuestOneCompleted)
                {
                    TriggerDialogueIndex(0);
                    m_QuestOneDialogue = false;
                }
                else if (m_QuestTwoDialogue || !m_QuestTwoCompleted) // If quest 2 hasnt been completed
                {
                    TriggerDialogueIndex(1);
                    m_QuestTwoDialogue = false;
                }
                else
                {
                    //If both are completed
                    TriggerDialogueIndex(2);
                }
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            {
                //DialogueManager.Instance.DisplayNextSentence();
            }

        }
        else
        {


        }

    }
    public void TriggerFirstTimeDialogue()
    {
        DialogueManager.Instance.StartDialogue(m_Dialogue[0], this);
    }

    public void TriggerRepeatDialogue()
    {
        DialogueManager.Instance.StartDialogue(m_Dialogue[1], this);
    }

    public void TriggerDialogueIndex(int _Index)
    {
        DialogueManager.Instance.StartDialogue(m_Dialogue[_Index], this);
    }
}
