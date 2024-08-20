using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV0_WASDMOVE : MonoBehaviour
{
    //if WASD are all pressed
    public bool W = false;
    public bool A = false;
    public bool S = false;
    public bool D = false;
    public bool taskFinished = false;

    public int taskIndex = 0;

    public TaskTracker taskTracker;
    public InGameHud inGameHud;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            W = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            A = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            S = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            D = true;
        }

        if (W && A && S && D)
        {
            if (!taskFinished)
            {
                taskFinished = true;
                inGameHud.showHud();
                taskTracker.completeObjective(taskIndex - 1);
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
            taskTracker = GameObject.Find("TaskTracker").GetComponent<TaskTracker>();
            inGameHud = GameObject.Find("InGameHud").GetComponent<InGameHud>();
    }

    // Update is called once per frame

}
