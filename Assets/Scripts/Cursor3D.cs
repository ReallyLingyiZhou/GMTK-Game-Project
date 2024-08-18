using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor3D : MonoBehaviour
{

    public ObjectSelectable objectSelectable;
    public Transform objectGrabbed; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //when E key is press
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleObjectSelection();
        }

        // when mouse right key released
        if (Input.GetMouseButtonUp(1))
        {
            ungrabObject();
        }
    }

    public void toggleObjectSelection(){
        Debug.Log("Toggle Object Selection");
        if(objectGrabbed == null){
            grabObject();
            Debug.Log("Grab Object");
        }
        else{
            ungrabObject();
            Debug.Log("Ungrab Object");
        }
    }

    public void grabObject()
    {
        if(objectGrabbed != null)  return;
    
        if(objectSelectable != null)
        {
            objectSelectable.tool_selected();
            objectGrabbed = objectSelectable.transform;
        }
    }
    public void ungrabObject()
    {
        if(objectGrabbed != null)
        {
            objectGrabbed.GetComponent<ObjectSelectable>().tool_deselected();
            objectGrabbed = null;
        }
    }










    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            hoverObject(other.gameObject.GetComponent<ObjectSelectable>());  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            unhoverObject(other.gameObject.GetComponent<ObjectSelectable>());
        }
    }

    public void hoverObject(ObjectSelectable obj) 
    {
        if (objectSelectable != null)
        {
            return;
        }
        objectSelectable = obj;
        objectSelectable.tool_hoverEnter();

    }
    public void unhoverObject(ObjectSelectable obj)
    {
        if(objectSelectable == obj)
        {
            objectSelectable.tool_hoverExit();
            objectSelectable = null;
        }
    }

    

}
