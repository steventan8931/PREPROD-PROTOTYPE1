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

    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI rockText;
    public TextMeshProUGUI currAmount;
    public TextMeshProUGUI requiredAmount;
    private void Awake()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        CurrQuest = quests[0];
    }
    private void Update()
    {
        updateAmount();

        if(CurrQuest.goal.IsReached() == true)
        {
            CurrQuest.isCompleted = true;
            playerInventory.m_WoodCount += CurrQuest.woodReward;
            playerInventory.m_RockCount += CurrQuest.rockReward;
            Debug.Log("quest completed!");
        }
        if(CurrQuest.isCompleted && questIndex < quests.Length)
        {
            questIndex++;
            CurrQuest = quests[questIndex];
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
