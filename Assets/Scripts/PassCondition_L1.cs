using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCondition_L1 : MonoBehaviour
{
    public bool passCondition_Destroy = false;
    public int taskIndex = 0;
    public GameObject levelEndingUI;
    public InGameHud inGameHud;
    public TaskTracker taskTracker;


    public string goalName = "Goal";
    public string goalBaseName = "Goal Base";
    // Start is called before the first frame update
    void Start()
    {
        if (inGameHud == null)
        {
            inGameHud = GameObject.Find("InGameHud").GetComponent<InGameHud>();
        }

        if (taskTracker == null)
        {
            taskTracker = GameObject.Find("TaskTracker").GetComponent<TaskTracker>();
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == goalBaseName)
        {
            levelEndingScenee();
        }
    }

    public void levelEndingScenee(bool IsCompleted = true)
    {
        inGameHud.showHud();
        taskTracker.completeObjective(taskIndex - 1, IsCompleted);
    }

    private void OnDestroy()
    {
        if (passCondition_Destroy)
        {
            levelEndingScenee(false);
        }
    }

}
