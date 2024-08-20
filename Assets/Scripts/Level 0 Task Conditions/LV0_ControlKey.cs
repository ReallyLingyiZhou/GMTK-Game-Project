using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV0_ControlKey : MonoBehaviour
{

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
        //lif eft control key released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!taskFinished)
            {
                taskFinished = true;
                inGameHud.showHud();
                taskTracker.completeObjective(taskIndex - 1);
            }
        }

    }
}
