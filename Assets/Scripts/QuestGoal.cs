using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;
    public Inventory m_Inventory;
    public Crafting m_Crafting;

    public GameObject m_NPC;
    public GameObject m_Tent;
    public DialogueTrigger cacheNPC;
    public DialogueTrigger playerDialogue;
    public DayNightScr dayNight;
    public bool IsReached()
    {
        switch (goalType)
        {
            case GoalType.GatheringWood:
                return (m_Inventory.m_WoodCount >= requiredAmount);
            case GoalType.GatheringRock:
                return (m_Inventory.m_RockCount >= requiredAmount);
            case GoalType.CraftWeapon:
                return (m_Inventory.m_PickaxeCount > 0 || m_Inventory.m_AxeCount > 0);
            case GoalType.CraftBedroll:
                return (m_Inventory.m_BedrollCount > 0);
            case GoalType.PlaceBedroll:
                return (m_Inventory.GetComponent<CharacterMotor>().SpawnPointChanged());
            case GoalType.CraftCampfire:
                return (m_Inventory.m_FireplaceCount > 0);
            case GoalType.SurviveTheNight: //If it is a day time
                return (!dayNight.isNight);
            case GoalType.TalktoNPC:
                if (cacheNPC)
                {
                    return (!cacheNPC.m_QuestOneDialogue);
                }
                else
                {
                    return false;
                }
            case GoalType.BuildTent:
                return (m_Inventory.m_TentCount > 0);
            case GoalType.TalkToNPC2: 
                if (cacheNPC)
                {
                    return (!cacheNPC.m_QuestTwoDialogue);
                }
                else
                {
                    return false;
                }
            case GoalType.PlaceNPCHouse:
                return (m_Inventory.m_NPCHouseCount <= 0);
            case GoalType.GoInsideTent:
                break;
        }

        return (currentAmount >= requiredAmount);
    }

    public void WoodGathered()
    {
        if(goalType == GoalType.GatheringWood)

           currentAmount++;
    }

    public void RockGathered()
    {

        if (goalType == GoalType.GatheringRock)
        {
            Debug.Log("gathered rock!");
            currentAmount++;
        }
    }

    public void MeleeEnemyKilled()
    {
        if (goalType == GoalType.KillMelee)
            currentAmount++;
    }

    public void RangedEnemyKilled()
    {
        if (goalType == GoalType.KillRanged)
            currentAmount++;
    }

    public void GiveReward()
    {
        switch (goalType)
        {
            case GoalType.GatheringWood:
                m_Inventory.m_Unlocked = true;
                playerDialogue.TriggerDialogueIndex(1); //Open inventory to see items
                break;
            case GoalType.GatheringRock:
                m_Crafting.UnlockCrafting();
                playerDialogue.TriggerDialogueIndex(2); //see what i can craft
                break;
            case GoalType.CraftWeapon:
                playerDialogue.TriggerDialogueIndex(3); //I should make a bedroll
                break;
            case GoalType.CraftBedroll:
                //set spawn point 
                break;
            case GoalType.PlaceBedroll:
                //Auto turns it to night 
                playerDialogue.TriggerDialogueIndex(4);
                break;
            case GoalType.CraftCampfire: //Create campfire to not lose health at night 
                dayNight.switchToNight();
                break;
            case GoalType.SurviveTheNight: //If is day, spawn the npc
                m_NPC.SetActive(true);
                cacheNPC = m_NPC.GetComponent<DialogueTrigger>();
                break;
            case GoalType.TalktoNPC: //Changes quest to build tent
                m_Tent.SetActive(true);
                break;
            case GoalType.BuildTent:
                cacheNPC.m_QuestOneDialogue = false;
                cacheNPC.m_QuestOneCompleted = true;
                break;
            case GoalType.TalkToNPC2: //Ill come live with you, choose where you want me to stay
                //Gives player the house
                m_Inventory.m_NPCHouseCount++;
                break;
            case GoalType.PlaceNPCHouse:
                cacheNPC.m_QuestTwoDialogue = false;
                cacheNPC.m_QuestTwoCompleted = true;
                break;
            case GoalType.GoInsideTent:
                //Unlock ability to go inside tent
                break;
        }

    }
}

public enum GoalType
{
    GatheringWood,
    GatheringRock,
    KillMelee,
    KillRanged,

    //gather wood ->unlocks inventory
    //gather rock -> unlock crafting
    CraftWeapon,
    CraftBedroll,
    PlaceBedroll,
    TalktoNPC,
    CraftCampfire,
    BuildTent,
    GoInsideTent,
    
    SurviveTheNight,
    TalkToNPC2,
    PlaceNPCHouse,
}
