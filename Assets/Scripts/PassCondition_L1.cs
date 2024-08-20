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
    public bool passCondition_Destroy = false; 
    public int taskIndex = 0; 

    public string goalName = "Goal";
    public string goalBaseName = "Goal Base";
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
        Debug.Log("Collision with " + other.gameObject.name);
        if(other.gameObject.name == goalName){
            itemPlaced = true;
        }
        if(other.gameObject.name == goalBaseName){
            levelEndingScenee();
            // itemStay = true;
            // if(itemPlaced){
                
            // }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == goalName){
            itemPlaced = false;
        }
        if(other.gameObject.name == goalBaseName){
            itemStay = false;
        }
    }

    public void levelEndingScenee(){
        inGameHud.showHud();
        taskTracker.completeObjective(taskIndex - 1 );
    }

    private void OnDestroy() {
        if(passCondition_Destroy){
            levelEndingScenee();
        }
    }

    
}
