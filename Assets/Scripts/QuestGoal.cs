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
    public DialogueTrigger cacheNPC;
    public DialogueTrigger playerDialogue;

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
            case GoalType.TalktoNPC:
                if (cacheNPC)
                {
                    return (!cacheNPC.m_FirstTimeDialogue);
                }
                else
                {
                    return false;
                }
            case GoalType.CraftCampfire:
                return (m_Inventory.m_FireplaceCount > 0);
            case GoalType.BuildTent:
                return (m_Inventory.m_TentCount > 0);
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
                playerDialogue.TriggerDialogueIndex(4); //set spawn point
                break;
            case GoalType.PlaceBedroll:
                m_NPC.SetActive(true);
                cacheNPC = m_NPC.GetComponent<DialogueTrigger>();
                break;
            case GoalType.TalktoNPC:
                break;
            case GoalType.CraftCampfire:
                break;
            case GoalType.BuildTent:
                break;
            case GoalType.GoInsideTent:
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
}
