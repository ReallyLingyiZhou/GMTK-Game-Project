using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject endMenuUi;
    public TaskSummery TaskSummery = new TaskSummery();
    [Range(.1f, int.MaxValue)]
    public float ParLevelTime = 180;
    public void OpenEndMenu(TaskSummery taskSummery)
    {
        TaskSummery = taskSummery;
        endMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public int GetTotalObjectives() =>
        TaskSummery.ObjectivesCompletedCount + TaskSummery.ObjectivesCompletedCount;
    public float GetBreakagePercentage() =>
        (GetTotalObjectives() + 1) / (TaskSummery.ObjectivesCompletedCount + TaskSummery.DestroyedCount + 1);
    public float GetScorePercentage() => (
            ((ParLevelTime + .1f - Math.Min(ParLevelTime, TaskSummery.time)) / ParLevelTime) +
            (1.0f / GetBreakagePercentage()) +
            ((TaskSummery.ObjectivesCompletedCount + 1) / (GetTotalObjectives() + 1))
        ) / 3;


    private void Resume()
    {
        endMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Resume();
    }

    public void Reset()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Resume();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Resume();
    }
}
