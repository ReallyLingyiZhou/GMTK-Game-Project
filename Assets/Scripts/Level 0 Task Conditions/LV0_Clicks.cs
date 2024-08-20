using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV0_Clicks : MonoBehaviour
{
    
    public bool leftClick = true;
    

    public int taskIndex = 0;

    public bool taskFinished = false;
    public TaskTracker taskTracker;
    public InGameHud inGameHud;


    void Start()
    {

        taskTracker = GameObject.Find("TaskTracker").GetComponent<TaskTracker>();
        inGameHud = GameObject.Find("InGameHud").GetComponent<InGameHud>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (leftClick)
            {
                if (!taskFinished)
                {
                    taskFinished = true;
                    inGameHud.showHud();
                    taskTracker.completeObjective(taskIndex - 1);
                }
            }
        }else if(Input.GetMouseButtonUp(1))
        {
            if (!leftClick)
            {
                if (!taskFinished)
                {
                    taskFinished = true;
                    inGameHud.showHud();
                    taskTracker.completeObjective(taskIndex - 1);
                    Debug.Log("Task Finished" + transform.name);
                }
            }
        }
    }
}
