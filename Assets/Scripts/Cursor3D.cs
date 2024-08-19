using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor3D : MonoBehaviour
{

    public ObjectSelectable objectSelectable;
    public Transform objectGrabbed;
    public float crounchVerticleOffset = 0.5f;
    public float originalZ;
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
            float duration = 0.5f; // Adjust the duration as needed

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
        if (objectSelectable == obj)
        {
            objectSelectable.tool_hoverExit();
            objectSelectable = null;
        }
    }



}
