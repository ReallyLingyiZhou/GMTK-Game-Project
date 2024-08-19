using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHud : MonoBehaviour
{
    //press m to toggle on and off an array of objects
    public GameObject[] hudObjects;
    public SpawnStrikethrough taskM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            toggleHud();
            if(taskM != null){
                taskM.taskFinished = true;
            }
        }

    }

    public void toggleHud(){
        foreach(GameObject obj in hudObjects){
            obj.SetActive(!obj.activeSelf);
        }
    }   

    public void showHud(){
        foreach(GameObject obj in hudObjects){
            obj.SetActive(true);
        }
    }
}
