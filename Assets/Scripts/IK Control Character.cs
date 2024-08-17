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
    private Coroutine rightHandCoroutine;
    public bool RightHandActive{
        get{

            return rightHandActive;
        }
        set{

            rightHandActive = value;
            if(rightHandActive){
                if(rightHandCoroutine != null)
                StopCoroutine(rightHandCoroutine);
                rightHandCoroutine = StartCoroutine(RaiseRightHand());

                
            }else{
                if(rightHandCoroutine != null)
                StopCoroutine(rightHandCoroutine);
                rightHandCoroutine = StartCoroutine(LowerRightHand());
            }
        }
    }

    private IEnumerator RaiseRightHand(){
        float progressPercentage = 0f;
        float startTime = Time.time;
        while(Time.time - startTime < raiseHandAnimationTimeDuration){
            progressPercentage = (Time.time - startTime) / raiseHandAnimationTimeDuration;
            animationPositionWeight_right = Mathf.Lerp(0f,animationPositionWeightMax,progressPercentage);
            animationRotationWeight_right = Mathf.Lerp(0f,animationRotationWeightMax,progressPercentage);
            yield return null;
        }
    }

    private IEnumerator LowerRightHand(){
        float progressPercentage = 0f;
        float startTime = Time.time;
        while(Time.time - startTime < raiseHandAnimationTimeDuration){
            progressPercentage = (Time.time - startTime) / raiseHandAnimationTimeDuration;
            Debug.Log(progressPercentage);
            animationPositionWeight_right = Mathf.Lerp(animationPositionWeightMax,0f,progressPercentage);
            Debug.Log(animationPositionWeight_right);
            animationRotationWeight_right = Mathf.Lerp(animationRotationWeightMax,0f,progressPercentage);
            yield return null;
        }
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

            //if the IK is active, set the position and rotation directly to the goal.
            if(RightHandActive) {

                // Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }else{
                    lookObj = rightHandCursor;
                }

                // Set the right hand target position and rotation, if one has been assigned
                if(rightHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,animationPositionWeight_right);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,animationRotationWeight_right);  
                    animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandCursor.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandCursor.rotation);
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