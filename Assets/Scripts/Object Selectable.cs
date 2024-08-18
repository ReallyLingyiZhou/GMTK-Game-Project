using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSelectable : MonoBehaviour
{
    //colors
    public Color hoverColor = Color.green;
    public Color selectedColor = Color.red;
    private Color defaultColor;

    public bool isGrabbed = false; 
    public Transform cursor3D; 
    public Vector3 offset; 
       
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }

    public void tool_hoverEnter(){
        GetComponent<Renderer>().material.color = hoverColor;
    }

    public void tool_hoverExit(){
        GetComponent<Renderer>().material.color = defaultColor;
    }

    public void tool_selected(){
        //disable collider
        GetComponent<Collider>().enabled = false;
        isGrabbed = true;
        offset = transform.position - cursor3D.position;
    }

    public void tool_deselected(){

        isGrabbed = false;
        offset = Vector3.zero;
        //enable collider
        GetComponent<Collider>().enabled = true;
    }   

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed){
            transform.position = cursor3D.position + offset;
        }
    }

}
