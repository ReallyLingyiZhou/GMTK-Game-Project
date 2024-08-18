using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// require rigidbody and collider
/// </summary>
/// 

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ObjectSelectable : MonoBehaviour
{
    //colors
    public Color hoverColor = Color.green;
    public Color selectedColor = Color.red;
    private Color defaultColor;

    public bool isGrabbed = false; 
    public Transform cursor3D; 
    public Cursor3D cursorScript;
    public Vector3 offset; 
       
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
        cursorScript = cursor3D.GetComponent<Cursor3D>();
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
        //disable rigidbody
        GetComponent<Rigidbody>().isKinematic = true;

        isGrabbed = true;
        offset = transform.position - cursor3D.position;
    }

    public void tool_deselected(){

        isGrabbed = false;
        offset = Vector3.zero;
        //enable collider
        GetComponent<Collider>().enabled = true;
        //enable rigidbody
        GetComponent<Rigidbody>().isKinematic = false;
    }   

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed){
            transform.position = cursor3D.position + offset;
        }
    }

}
