using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCondition_L1 : MonoBehaviour
{

    //Pass Condition for Level 1
    //1. Item is place in the target location, Collision enter with the "Goal" nnamed object
    //2. Item is dropped. Mouse click up


    public bool itemPlaced = false;
    public bool itemDropped = false;
    public bool itemStay = false;

    public GameObject levelEndingUI;
    public InGameHud inGameHud;
    public TaskTracker taskTracker;
    
    // Start is called before the first frame update
    void Start()
    {
        if(inGameHud == null){
            inGameHud = GameObject.Find("InGameHud").GetComponent<InGameHud>();
        }

        if(taskTracker == null){
            taskTracker = GameObject.Find("TaskTracker").GetComponent<TaskTracker>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonUp(1)){
            itemDropped = true;
            
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Goal"){
            itemPlaced = true;
        }
        if(other.gameObject.name == "Goal Base"){
            itemStay = true;
            if(itemDropped && itemPlaced){
                levelEndingScenee();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "Goal"){
            itemPlaced = false;
        }
        if(other.gameObject.name == "Goal Base"){
            itemStay = false;
        }
    }

    public void levelEndingScenee(){
        inGameHud.showHud();
        taskTracker.completeObjective(0);
        taskTracker.completeObjective(1);
        taskTracker.completeObjective(2);
        taskTracker.completeObjective(3);
    }

    
}
