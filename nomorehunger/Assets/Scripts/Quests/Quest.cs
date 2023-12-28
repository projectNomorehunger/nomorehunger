using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestProgress {NOT_AVAILABLE, AVAILABLE, ACCEPTED, COMPLETE,DONE }

    public string title;            //title for the quest
    public int id;                  //ID
    public QuestProgress progress;  //state of the current quest (enum)
    public string description;      //string from our quest Giver/Receiver
    public string hint;             //string from our quest Giver/Receiver
    public string congratulation;   //string from our quest Giver/Receiver
    public string summary;          //string from our quest Giver/Receiver
    public int nextQuest;           // the next quest id

    public string questObjective;   //name of the quest objective(also for remove)
    public int questObjectiveCount; // currrent number of questObjective count
    public int questObjectiveRequirement;// required amount of quest objects

    public int expReward;
    public int goldReward;
    public string itemReward;
}
