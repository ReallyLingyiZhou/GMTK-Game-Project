using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using System;

public class TaskTrackerDebug : MonoBehaviour
{
    public float time;
    public int ObjectivesCompletedCount;
    public int ObjectivesDestroyedCount;
    public int TotalObjectives;
    public float BreakagePercentage;
    public float ScorePercentage;
    public int DestroyedCount;
    public TaskTracker taskTracker;
    void Start() => PlaceHolderName();
    void Update() => PlaceHolderName();
    private void PlaceHolderName()
    {
        var tts = taskTracker.GetAllTaskSummery();
        time = tts.time;
        ObjectivesCompletedCount = tts.ObjectivesCompletedCount;
        ObjectivesDestroyedCount = tts.ObjectivesDestroyedCount;
        DestroyedCount = tts.DestroyedCount;

        TotalObjectives = GetTotalObjectives(tts);
        BreakagePercentage = GetBreakagePercentage(tts);
        ScorePercentage = GetScorePercentage(tts);
    }
    public int GetTotalObjectives(TaskSummery tts) =>
        tts.TotalObjectives;
    public float GetBreakagePercentage(TaskSummery tts) =>
        (GetTotalObjectives(tts) + 1) / (tts.ObjectivesCompletedCount + tts.DestroyedCount + 1);
    public float GetScorePercentage(TaskSummery tts) => (
            ((180 + .1f - Math.Min(180, tts.time)) / 180) +
            (1.0f / GetBreakagePercentage(tts)) +
            ((tts.ObjectivesCompletedCount + 1) / (GetTotalObjectives(tts) + 1))
        ) / 3;

}
