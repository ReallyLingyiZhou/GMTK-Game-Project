using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelectable : MonoBehaviour
{
    //colors
    public Color hoverColor = Color.green;
    public Color selectedColor = Color.red;
    private Color defaultColor;
    public IKControlCharacter ikControlCharacter;

    public Transform lookAtRoot; 
       
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
        if (ikControlCharacter == null)
        {
            // ikControlCharacter = FindObjectOfType<IKControlCharacter>();
        }
    }

    // //when hovered by mouse
    // void OnMouseEnter()
    // {
    //     GetComponent<Renderer>().material.color = hoverColor;
    //     ikControlCharacter.initiateTargetObject(transform);
    // }

    // void OnMouseExit()
    // {
    //     GetComponent<Renderer>().material.color = defaultColor;
    // }


}
