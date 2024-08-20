using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV0_EPress : MonoBehaviour
{
    public int taskIndex = 0;

    public bool taskFinished = false;
    public TaskTracker taskTracker;
    public InGameHud inGameHud;

    public bool isGrabbed = false;

    public ObjectSelectable objectSelectable;
    void Start()
    {

        taskTracker = GameObject.Find("TaskTracker").GetComponent<TaskTracker>();
        inGameHud = GameObject.Find("InGameHud").GetComponent<InGameHud>();
        objectSelectable = GetComponent<ObjectSelectable>();
    }

    // Update is called once per frame
    void Update()
    {
        //if E key is pressed
       
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E Pressed");
            if (!taskFinished)
            {
                if(isGrabbed)
                {
                    taskFinished = true;
                    inGameHud.showHud();
                    taskTracker.completeObjective(taskIndex - 1);
                }
                
            }
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "LeftHandCursor" || other.name == "RightHandCursor")
        {
            isGrabbed = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        isGrabbed = false;
    }
}
