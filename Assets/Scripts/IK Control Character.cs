using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControlCharacter : MonoBehaviour
{
    protected Animator animator;

    public float raiseHandAnimationTimeDuration = 0.2f;
    public float animationPositionWeightMax = 1f;
    public float animationRotationWeightMax = 0.5f;

    private float animationPositionWeight_right = 0f; 
    private float animationRotationWeight_right = 0f; 

    public Transform rightHandCursor; 


    private bool rightHandActive = false; 
    public bool IKActive_Right = false; 
    private Coroutine rightHandCoroutine;
    public bool RightHandActive{
        get{

            return rightHandActive;
        }
        set{

            rightHandActive = value;
            if(rightHandActive){
                if(rightHandCoroutine != null){
                    StopCoroutine(rightHandCoroutine);      
                }
                
                rightHandCoroutine = StartCoroutine(RaiseRightHand());

                
            }else{
                if(rightHandCoroutine != null){
                    StopCoroutine(rightHandCoroutine);
                }
                rightHandCoroutine = StartCoroutine(LowerRightHand());
            }
        }
    }

    private IEnumerator RaiseRightHand(){
        IKActive_Right = true;
        float progressPercentage = 0f;
        float startTime = Time.time;

        float startWeightRotation = animationRotationWeight_right;
        float startWeightPosition = animationPositionWeight_right;
        while(Time.time - startTime < raiseHandAnimationTimeDuration){
            progressPercentage = (Time.time - startTime) / raiseHandAnimationTimeDuration;
            animationPositionWeight_right = Mathf.Lerp(startWeightPosition,animationPositionWeightMax,progressPercentage);
            animationRotationWeight_right = Mathf.Lerp(startWeightRotation,animationRotationWeightMax,progressPercentage);
            yield return null;
        }

        IKActive_Right = false;
    }

    private IEnumerator LowerRightHand(){
        IKActive_Right = true;
        float progressPercentage = 0f;
        float startTime = Time.time;
        Vector3 cursorStartPosition = rightHandCursor.localPosition;
        float startWeightRotation = animationRotationWeight_right;
        float startWeightPosition = animationPositionWeight_right;
        while(Time.time - startTime < raiseHandAnimationTimeDuration){
            progressPercentage = (Time.time - startTime) / raiseHandAnimationTimeDuration;
            animationPositionWeight_right = Mathf.Lerp(startWeightPosition,0f,progressPercentage);
            animationRotationWeight_right = Mathf.Lerp(startWeightRotation,0f,progressPercentage);
            rightHandCursor.localPosition = Vector3.Lerp(cursorStartPosition,Vector3.zero,progressPercentage);
            yield return null;
        }

        IKActive_Right = false;

    }
    
    public void resetRightHandCursor(){
        rightHandCursor.localPosition = Vector3.zero;
    }

    public bool ikActive = false;
    public float sensitivity = 0.1f;    
    [HideInInspector]
    public Transform rightHandObj = null;
    [HideInInspector]
    public Transform leftHandObj = null;
    [HideInInspector]
    public Transform lookObj = null;

    void Start ()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if(animator) {
            if(IKActive_Right){
                IKRightHand();
            }


            //if the IK is active, set the position and rotation directly to the goal.
            // if(RightHandActive) {

            //     // Set the look target position, if one has been assigned
            //     if(lookObj != null) {
            //         animator.SetLookAtWeight(1);
            //         animator.SetLookAtPosition(lookObj.position);
            //     }else{
            //         lookObj = rightHandCursor;
            //     }

            //     // Set the right hand target position and rotation, if one has been assigned
            //     if(rightHandObj != null) {
            //       IKRightHand(); 
            //     }

            // }

            // //if the IK is not active, set the position and rotation of the hand and head back to the original position
            // else {          
            //     Debug.Log(animationPositionWeight_right);
            //     animator.SetIKPositionWeight(AvatarIKGoal.RightHand,animationPositionWeight_right);
            //     animator.SetIKRotationWeight(AvatarIKGoal.RightHand,animationRotationWeight_right);
            //     animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandCursor.position);
            //     animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandCursor.rotation);
            //     animator.SetLookAtWeight(animationRotationWeight_right);
            // }
        }
    }

    public void IKRightHand(){
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand,animationPositionWeight_right);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand,animationRotationWeight_right);  
        animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandCursor.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandCursor.rotation);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RightHandActive = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            RightHandActive = false;
        }

        if (RightHandActive)
        {
            // Get the mouse movement along the X and Y axes
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Calculate the new position of the look object
            Vector3 lookObjPosition = rightHandCursor.position;
            lookObjPosition += transform.right * mouseX * sensitivity;
            lookObjPosition += transform.forward * mouseY * sensitivity;

            // Set the new position of the look object
            rightHandCursor.position = lookObjPosition;
        }
    }

    
}