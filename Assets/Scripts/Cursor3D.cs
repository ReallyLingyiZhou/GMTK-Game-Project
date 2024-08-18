using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor3D : MonoBehaviour
{
    public Transform selectedObject = null;
    public ObjectSelectable objectSelectable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //when E key is press
        if(Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("E key pressed");
            if(selectedObject != null) {
                if(objectSelectable.isGrabbed) {
                    objectSelectable.tool_deselected();
                } else {
                    objectSelectable.tool_selected();
                }
            }
        }

        // when mouse right key released
        if(Input.GetMouseButtonUp(1)) {
            Debug.Log("Right mouse button released");
            if(selectedObject != null) {
                objectSelectable.tool_deselected();
            }
        }
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Interactable") {
            selectedObject = other.transform;
            objectSelectable = selectedObject.GetComponent<ObjectSelectable>();
            objectSelectable.tool_hoverEnter();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Interactable") {
            objectSelectable.tool_hoverExit();
            selectedObject = null;
            objectSelectable = null;
        }
    }
}
