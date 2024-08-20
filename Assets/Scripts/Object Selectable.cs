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
    public Transform holdingPoint; 
    public Vector3 offset; 

    
       
    // Start is called before the first frame update
    void Start()
    {
       
        defaultColor = GetComponent<Renderer>().material.GetColor("_Color0");
        if(cursor3D == null){
            cursor3D = GameObject.Find("RightHandCursor").transform;
        }

        cursorScript = cursor3D.GetComponent<Cursor3D>();
    }

    public void tool_hoverEnter(){
        
        GetComponent<Renderer>().material.color = hoverColor;
        //in the shader it's called _Color0, change it to hoverColor
        GetComponent<Renderer>().material.SetColor("_Color0", hoverColor);

    }

    public void tool_hoverExit(){
        GetComponent<Renderer>().material.color = defaultColor;
        //in the shader it's called _Color0, change it to defaultColor
        GetComponent<Renderer>().material.SetColor("_Color0", defaultColor);
    }

    public void tool_selected(){
        //disable collider
        GetComponent<Collider>().enabled = false;
        //disable rigidbody
        GetComponent<Rigidbody>().isKinematic = true;

        isGrabbed = true;
        offset = transform.position - cursor3D.position;
        this.transform.parent = holdingPoint;
    }

    public void tool_deselected(){

        isGrabbed = false;
        offset = Vector3.zero;
        //enable collider
        GetComponent<Collider>().enabled = true;
        //enable rigidbody
        GetComponent<Rigidbody>().isKinematic = false;
        this.transform.parent = null;
    }   

    // Update is called once per frame
    void Update()
    {
        // if(isGrabbed){
        //     // transform.position = cursor3D.position + offset;
            
        //     //set the position of the object to the position of the cursor, but rotate the offset based on the rotation of the cursor
        //     transform.position = cursor3D.position + cursor3D.rotation * offset;

        // }
    }

}
