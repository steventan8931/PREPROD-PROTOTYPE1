using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
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
            currentAmount++;
    }

    public void EnemyKilled()
    {
        if (goalType == GoalType.Kill)
            currentAmount++;
    }
}

public enum GoalType
{
    GatheringWood,
    GatheringRock,
    Kill
}
