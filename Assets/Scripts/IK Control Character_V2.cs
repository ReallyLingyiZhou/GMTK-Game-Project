using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class IKControlCharacter_V2 : MonoBehaviour
{
    protected Animator animator;

    public Transform rightHandCursor;
    public float animationTimeDuration = 1f;
    public float sensitivity = 0.1f;

    public bool rightHandActive = false;
    public bool rightHandAnimation_Active = false;
    public bool rightHandGrabbing = false;

    public float maxWeight_rightPosition = 1f;
    public float maxWeight_rightRotation = 1f;
    public float animationPositionWeight_right = 0;
    public float animationRotationWeight_right = 0;
    public IEnumerator rightHandAnimationCoroutine;
    public bool RightHandActive
    {
        get
        {
            return rightHandActive;
        }
        set
        {
            rightHandActive = value;

            if (rightHandActive)
            {
                rightHandAnimation_Active = true;
                if (rightHandAnimationCoroutine != null)
                {
                    StopCoroutine(rightHandAnimationCoroutine);
                }
                rightHandAnimationCoroutine = RaiseHandAnimation();
                StartCoroutine(rightHandAnimationCoroutine);
            }
            else
            {
                rightHandAnimation_Active = false;
                if (rightHandAnimationCoroutine != null)
                {
                    StopCoroutine(rightHandAnimationCoroutine);
                }
                rightHandAnimationCoroutine = LowerHandAnimation();
                StartCoroutine(rightHandAnimationCoroutine);
            }
        }
    }


    public IEnumerator RaiseHandAnimation()
    {
        rightHandAnimation_Active = true;
        float progress = 0f;
        float startTime = Time.time;


        float startWeightRotation = animationRotationWeight_right;
        float startWeightPosition = animationPositionWeight_right;
        while (Time.time - startTime < animationTimeDuration)
        {
            progress = (Time.time - startTime) / animationTimeDuration;
            animationPositionWeight_right = Mathf.Lerp(startWeightPosition, maxWeight_rightPosition, progress);
            animationRotationWeight_right = Mathf.Lerp(startWeightRotation, maxWeight_rightRotation, progress);
            yield return null;
        }

        animationPositionWeight_right = maxWeight_rightPosition;
        animationRotationWeight_right = maxWeight_rightRotation;

        rightHandGrabbing = true;
    }

    public IEnumerator LowerHandAnimation()
    {
        rightHandGrabbing = false;
        rightHandAnimation_Active = true;
        float progress = 0f;
        float startTime = Time.time;

        float startWeightRotation = animationRotationWeight_right;
        float startWeightPosition = animationPositionWeight_right;

        Vector3 cursorStartPosition = rightHandCursor.localPosition;
        while (Time.time - startTime < animationTimeDuration)
        {
            progress = (Time.time - startTime) / animationTimeDuration;
            animationPositionWeight_right = Mathf.Lerp(startWeightPosition, 0, progress);
            animationRotationWeight_right = Mathf.Lerp(startWeightRotation, 0, progress);
            rightHandCursor.localPosition = Vector3.Lerp(cursorStartPosition, Vector3.zero, progress);
            yield return null;
        }

        animationPositionWeight_right = 0;
        animationRotationWeight_right = 0;
        rightHandAnimation_Active = false;
        rightHandCursor.localPosition = Vector3.zero;
    }

    private float armLength = 0f; 
    public Transform rightShoulder, rightElbow, rightWrist;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        armLength = Vector3.Distance(rightShoulder.position, rightElbow.position) + Vector3.Distance(rightElbow.position, rightWrist.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RightHandActive = true;
            //disable mouse cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            RightHandActive = false;
            //enable mouse cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (rightHandGrabbing)
        {
            // Get the mouse movement along the X and Y axes
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Vector3 offset = new Vector3(mouseX, 0, mouseY);
            Vector3 cursorPosition = rightHandCursor.position;

            // Move the cursor
            rightHandCursor.position = cursorPosition + offset * sensitivity;
            // set the rotation based on the vector from shoulder to wrist on the XZ plane
            Vector3 shoulderToWrist = rightWrist.position - rightShoulder.position;
            
            rightHandCursor.rotation = Quaternion.LookRotation(shoulderToWrist);

        }
    }

    //fixed update
    private void FixedUpdate() {
        
    }




    void OnAnimatorIK()
    {
        if (rightHandAnimation_Active)
        {
            IKRightHand();
        }
    }

    public void IKRightHand()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, animationPositionWeight_right);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, animationRotationWeight_right);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandCursor.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandCursor.rotation);

        if (rightHandGrabbing)
        {
            //get distance from cursor to shoulder
            float distanceToShoulder = Vector3.Distance(rightHandCursor.position, rightShoulder.position);
            if(distanceToShoulder > armLength)
            {
                Vector3 cursorToShoulder = rightShoulder.position - rightHandCursor.position;
                cursorToShoulder.Normalize();
                rightHandCursor.position = rightShoulder.position - cursorToShoulder * armLength;
            }

        }
    }
}
