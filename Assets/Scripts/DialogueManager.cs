using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public GameObject m_DialogueCanvas;
    public Text m_NameText;
    public Text m_DialogueText;
    public Image m_Image;
    public Queue<string> m_Sentences;
    public GameObject m_QuickBar;

    private CharacterMotor m_Player;
    private DialogueTrigger cacheNPC;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        m_Sentences = new Queue<string>();

        m_Player = FindObjectOfType<CharacterMotor>();
        m_QuickBar = FindObjectOfType<QuickBar>().gameObject;
;
    }

    public void StartDialogue(Dialogue _Dialogue, DialogueTrigger m_NPC)
    {
        m_QuickBar.SetActive(false);
        m_Player.m_CanMove = false;
        cacheNPC = m_NPC;
        cacheNPC.m_IsTalking = true;
        m_DialogueCanvas.SetActive(true);
        m_NameText.text = _Dialogue.m_Name;
        m_Image.sprite = m_NPC.m_Sprite;

        m_Sentences.Clear();

        foreach (string sentence in _Dialogue.m_Sentences)
        {
            m_Sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (m_Sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = m_Sentences.Dequeue();
        m_DialogueText.text = sentence;
    }

    private void EndDialogue()
    {
        m_QuickBar.SetActive(true);
        cacheNPC.m_IsTalking = false;
        m_Player.m_CanMove = true;
        m_DialogueCanvas.SetActive(false);
    }


}
