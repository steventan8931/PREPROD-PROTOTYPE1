using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public Quest quest1;

    public Inventory playerInventory;

    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI rockText;
    private void Awake()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            openQuestWindow();
        }
    }
    public void openQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest1.title;
        descriptionText.text = quest1.description;
        woodText.text = quest1.woodReward.ToString();
        rockText.text = quest1.rockReward.ToString();
    }
}
