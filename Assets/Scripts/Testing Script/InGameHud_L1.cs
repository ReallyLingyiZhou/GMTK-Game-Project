using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHud_L1 : MonoBehaviour
{
    //press k key to test the hub
    public SpawnStrikethrough spawnStrikethrough;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            spawnStrikethrough.Spawn();
        }
    }

}
