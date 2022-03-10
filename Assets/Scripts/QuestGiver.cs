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

    DialogueTrigger cacheNPC;
    public DialogueTrigger playerDialogue;
    public DayNightScr m_DayNight;

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
        }
    }
    private void Start()
    {
        playerDialogue.TriggerDialogueIndex(0);
    }
    private void LinkNPC()
    {
        if (CurrQuest.goal.goalType >= GoalType.PlaceBedroll)
        {
            cacheNPC = CurrQuest.goal.cacheNPC;
        }
    }

    private void AddNPCtoQuest()
    {
        if (!CurrQuest.goal.cacheNPC && cacheNPC)
        {
            CurrQuest.goal.cacheNPC = cacheNPC;
        }
    }

    private void Update()
    {
        updateAmount();

        if(CurrQuest.goal.IsReached() == true)
        {
            CurrQuest.isCompleted = true;
            CurrQuest.goal.GiveReward();
            LinkNPC();
            //playerInventory.m_WoodCount += CurrQuest.woodReward;
            //playerInventory.m_RockCount += CurrQuest.rockReward;
            Debug.Log("quest completed!");
        }
        if(CurrQuest.isCompleted && questIndex < quests.Length - 1)
        {
            questIndex++;            
            CurrQuest = quests[questIndex];
            AddNPCtoQuest();
            refreshQuest();
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
    }

    void updateAmount()
    {
        currAmount.text = CurrQuest.goal.currentAmount.ToString();
        requiredAmount.text = CurrQuest.goal.requiredAmount.ToString();
    }
}
