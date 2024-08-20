using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor3D : MonoBehaviour
{

    public ObjectSelectable objectSelectable;
    public Transform objectGrabbed;
    public float crounchVerticleOffset = 0.5f;
    public float originalZ;
    public float reachDownAnimationDurationInSecond = 0.5f;
    public Transform holdingPoint;

    
    // Start is called before the first frame update
    void Start()
    {
        originalZ = transform.position.z;

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

        //Right mouse key released
        if (Input.GetMouseButtonUp(0))
        {
            ungrabObject();
        }

        //when ctrl is down
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(Crouch());
        }



        IEnumerator Crouch()
        {
            float elapsedTime = 0f;
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(transform.position.x, -crounchVerticleOffset, transform.position.z);
            float duration = reachDownAnimationDurationInSecond; // Adjust the duration as needed

            while (elapsedTime < duration)
            {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
            }

            transform.position = endPos;
        }

    }

    public void toggleObjectSelection()
    {
        Debug.Log("Toggle Object Selection");
        if (objectGrabbed == null)
        {
            grabObject();
            Debug.Log("Grab Object");
        }
        else
        {
            ungrabObject();
            Debug.Log("Ungrab Object");
        }
    }

    public void grabObject()
    {
        if (objectGrabbed != null) return;

        if (objectSelectable != null)
        {
            objectSelectable.tool_selected();
            objectGrabbed = objectSelectable.transform;
        }
    }
    public void ungrabObject()
    {
        if (objectGrabbed != null)
        {
            objectGrabbed.GetComponent<ObjectSelectable>().tool_deselected();
            objectGrabbed = null;
        }
    }

    //enable all 2 colliers and a rigidbody attached to this object
    public void enablePhysics()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
        
    }

    //disable all 2 colliers and a rigidbody attached to this object
    public void disablePhysics()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
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

        if(obj.isGrabbed)
        {
            return;
        }
        objectSelectable = obj;

        objectSelectable.cursor3D = this.transform;
        objectSelectable.cursorScript = this;
        objectSelectable.holdingPoint = holdingPoint;
        objectSelectable.tool_hoverEnter();

    }
    public void unhoverObject(ObjectSelectable obj)
    {
        if (objectSelectable == obj)
        {
            objectSelectable.tool_hoverExit();
            objectSelectable = null;
        }
    }



}
