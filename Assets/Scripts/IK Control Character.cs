using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControlCharacter : MonoBehaviour
{
   protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform lookObj = null;

    void Start ()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if(animator) {

            //if the IK is active, set the position and rotation directly to the goal.
            if(ikActive) {

                // Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if(rightHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
                    animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {          
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);
                animator.SetLookAtWeight(0);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ikActive = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            ikActive = false;
        }


        if (ikActive)
        {
            // Get the mouse movement along the X and Y axes
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Calculate the new position of the look object
            Vector3 lookObjPosition = lookObj.position;
            lookObjPosition += transform.right * mouseX;
            lookObjPosition += transform.forward * mouseY;

            // Set the new position of the look object
            lookObj.position = lookObjPosition;
        }
        
    }
}