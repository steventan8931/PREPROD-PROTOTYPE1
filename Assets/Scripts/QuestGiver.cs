using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestGiver : MonoBehaviour
{
    public Quest CurrQuest;
    public Quest[] quests;
    int questIndex = 0;
    public Inventory playerInventory;
    private Crafting m_CraftingManager; 

    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI rockText;
    public TextMeshProUGUI currAmount;
    public TextMeshProUGUI requiredAmount;

    public Text questTitle;
    public Text questDescription;
    public Image m_Stamp;

    public DialogueTrigger cacheNPC;
    public DialogueTrigger playerDialogue;
    public DayNightScr m_DayNight;
    public GameObject m_Table1;
    public GameObject m_Table2;
    public GameObject m_Cabin;

    public float m_NextQuestDelay = 1.0f;
    public float m_DelayTimer = 0.0f;

    private AudioManager audio;

    private void Awake()
    {
        m_DayNight = FindObjectOfType<DayNightScr>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        m_CraftingManager = FindObjectOfType<Crafting>();
        CurrQuest = quests[0];

        //Set up quest inventories
        foreach (Quest quest in quests)
        {
            quest.goal.m_Inventory = playerInventory;
            quest.goal.m_Crafting = m_CraftingManager;
            quest.goal.playerDialogue = playerDialogue;
            quest.goal.dayNight = m_DayNight;
            quest.goal.cacheNPC = cacheNPC;
            quest.goal.m_Table1 = m_Table1;
            quest.goal.m_Table2 = m_Table2;
            quest.goal.m_Cabin = m_Cabin;
        }

        //Temp new quest panel
        questTitle = questWindow.transform.GetChild(1).GetComponent<Text>();
        questDescription = questWindow.transform.GetChild(2).GetComponent<Text>();
        m_Stamp.enabled = false;
    }
    private void Start()
    {
        playerDialogue.TriggerDialogueIndex(0);
    }

    private void Update()
    {
        updateAmount();

        if(CurrQuest.goal.IsReached() == true)
        {
            CurrQuest.isCompleted = true;
            CurrQuest.goal.GiveReward();
            //LinkNPC();
            //playerInventory.m_WoodCount += CurrQuest.woodReward;
            //playerInventory.m_RockCount += CurrQuest.rockReward;
            Debug.Log("quest completed!");
        }
        if(CurrQuest.isCompleted && questIndex < quests.Length - 1)
        {
            if (!questWindow.activeInHierarchy)
            {
                openQuestWindow();
            }
            m_Stamp.enabled = true;
            if (m_DelayTimer <= 0)
            {
                AudioManager.Instance.PlayAudio("stamp");
            }
            m_DelayTimer += Time.deltaTime;
            if (m_DelayTimer > m_NextQuestDelay)
            {
                questIndex++;
                CurrQuest = quests[questIndex];
                //AddNPCtoQuest();
                refreshQuest();
                m_DelayTimer = 0;
                m_Stamp.enabled = false;
            }

        }
        if(Input.GetKeyDown(KeyCode.Q) )
        {
            openQuestWindow();
        }

      //  if (Input.GetKeyDown(KeyCode.M))
     //   {
    //        CurrQuest.isCompleted = true;
    //    }

    }
    public void openQuestWindow()
    {
        if (questWindow.activeSelf == false)
        {
            questWindow.SetActive(true);
            refreshQuest();
        }else
        {
            questWindow.SetActive(false);
        }
        
    }

    void refreshQuest()
    {
        titleText.text = CurrQuest.title;
        descriptionText.text = CurrQuest.description;
        woodText.text = CurrQuest.woodReward.ToString();
        rockText.text = CurrQuest.rockReward.ToString();

        questTitle.text = CurrQuest.title;
        questDescription.text = CurrQuest.description;

    }

    void updateAmount()
    {
        currAmount.text = CurrQuest.goal.currentAmount.ToString();
        requiredAmount.text = CurrQuest.goal.requiredAmount.ToString();
    }
}
