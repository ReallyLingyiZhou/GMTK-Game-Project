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
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        levelEndingUI.SetActive(true);
        //disable the character controller

    }

    
}
