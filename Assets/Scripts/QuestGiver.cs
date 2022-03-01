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
    private void Awake()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        CurrQuest = quests[0];
    }
    private void Update()
    {
        if(CurrQuest.goal.IsReached() == true)
        {
            CurrQuest.isCompleted = true;
            Debug.Log("quest completed!");
        }
        if(CurrQuest.isCompleted && questIndex < quests.Length)
        {
            questIndex++;
            CurrQuest = quests[questIndex];
            refreshQuest();
        }
        if(Input.GetKeyDown(KeyCode.Q))
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
        questWindow.SetActive(true);
        refreshQuest();
        
    }

    void refreshQuest()
    {
        titleText.text = CurrQuest.title;
        descriptionText.text = CurrQuest.description;
        woodText.text = CurrQuest.woodReward.ToString();
        rockText.text = CurrQuest.rockReward.ToString();
    }
}
