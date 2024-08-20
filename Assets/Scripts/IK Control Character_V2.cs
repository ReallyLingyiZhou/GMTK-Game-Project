using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class IKControlCharacter_V2 : MonoBehaviour
{
    protected Animator animator;
    public Cursor3D cursorLeft, cursorRight;

    public Transform rightHandCursor;
    public Transform leftHandCursor;
    public float animationTimeDuration = 1f;
    public float sensitivity = 0.1f;

    public float maxWeight_Position = 1f;
    public float maxWeight_Rotation = 1f;

    private float animationPositionWeight_right = 0;
    private float animationRotationWeight_right = 0;
    private bool rightHandActive = false;
    private bool rightHandAnimation_Active = false;
    private bool rightHandGrabbing = false;
    private IEnumerator rightHandAnimationCoroutine;
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
                cursorRight.enablePhysics();
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
                cursorRight.disablePhysics();
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


    private IEnumerator RaiseHandAnimation()
    {
        rightHandAnimation_Active = true;
        float progress = 0f;
        float startTime = Time.time;


        float startWeightRotation = animationRotationWeight_right;
        float startWeightPosition = animationPositionWeight_right;
        while (Time.time - startTime < animationTimeDuration)
        {
            progress = (Time.time - startTime) / animationTimeDuration;
            animationPositionWeight_right = Mathf.Lerp(startWeightPosition, maxWeight_Position, progress);
            animationRotationWeight_right = Mathf.Lerp(startWeightRotation, maxWeight_Rotation, progress);
            yield return null;
        }

        animationPositionWeight_right = maxWeight_Position;
        animationRotationWeight_right = maxWeight_Rotation;

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

    private float animationPositionWeight_left = 0;
    private float animationRotationWeight_left = 0;
    public bool leftHandActive = false;
    private bool leftHandAnimation_Active = false;
    private bool leftHandGrabbing = false;
    private IEnumerator leftHandAnimationCoroutine;

    public bool LeftHandActive
    {
        get
        {
            return leftHandActive;
        }
        set
        {
            leftHandActive = value;

            if (leftHandActive)
            {
                cursorLeft.enablePhysics();
                leftHandAnimation_Active = true;
                if (leftHandAnimationCoroutine != null)
                {
                    StopCoroutine(leftHandAnimationCoroutine);
                }
                leftHandAnimationCoroutine = RaiseHandAnimationLeft();
                StartCoroutine(leftHandAnimationCoroutine);
            }
            else
            {
                cursorLeft.disablePhysics();
                leftHandAnimation_Active = false;
                if (leftHandAnimationCoroutine != null)
                {
                    StopCoroutine(leftHandAnimationCoroutine);
                }
                leftHandAnimationCoroutine = LowerHandAnimationLeft();
                StartCoroutine(leftHandAnimationCoroutine);
            }
        }
    }

    private IEnumerator RaiseHandAnimationLeft()
    {
        leftHandAnimation_Active = true;
        float progress = 0f;
        float startTime = Time.time;

        float startWeightRotation = animationRotationWeight_left;
        float startWeightPosition = animationPositionWeight_left;
        while (Time.time - startTime < animationTimeDuration)
        {
            progress = (Time.time - startTime) / animationTimeDuration;
            animationPositionWeight_left = Mathf.Lerp(startWeightPosition, maxWeight_Position, progress);
            animationRotationWeight_left = Mathf.Lerp(startWeightRotation, maxWeight_Rotation, progress);
            yield return null;
        }

        animationPositionWeight_left = maxWeight_Position;
        animationRotationWeight_left = maxWeight_Rotation;

        leftHandGrabbing = true;
    }

    public IEnumerator LowerHandAnimationLeft()
    {
        leftHandGrabbing = false;
        leftHandAnimation_Active = true;
        float progress = 0f;
        float startTime = Time.time;

        float startWeightRotation = animationRotationWeight_left;
        float startWeightPosition = animationPositionWeight_left;

        Vector3 cursorStartPosition = leftHandCursor.localPosition;
        while (Time.time - startTime < animationTimeDuration)
        {
            progress = (Time.time - startTime) / animationTimeDuration;
            animationPositionWeight_left = Mathf.Lerp(startWeightPosition, 0, progress);
            animationRotationWeight_left = Mathf.Lerp(startWeightRotation, 0, progress);
            leftHandCursor.localPosition = Vector3.Lerp(cursorStartPosition, Vector3.zero, progress);
            yield return null;
        }

        animationPositionWeight_left = 0;
        animationRotationWeight_left = 0;
        leftHandAnimation_Active = false;
        leftHandCursor.localPosition = Vector3.zero;
    }










    public float armLength = 0f;
    public Transform rightShoulder, rightElbow, rightWrist;
    public Transform leftShoulder, leftElbow, leftWrist;
    // Start is called before the first frame update
    void Start()
    {
        cursorLeft = GameObject.Find("LeftHandCursor").GetComponent<Cursor3D>();
        cursorRight = GameObject.Find("RightHandCursor").GetComponent<Cursor3D>();
        cursorLeft.disablePhysics();
        cursorRight.disablePhysics();
        //set framerate to 60
        Application.targetFrameRate = 60;
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

        if (Input.GetMouseButtonDown(0))
        {
            LeftHandActive = true;
            //disable mouse cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            LeftHandActive = false;
            //enable mouse cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (leftHandGrabbing)
        {
            // Get the mouse movement along the X and Y axes
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Vector3 offset = new Vector3(mouseX, 0, mouseY);
            Vector3 cursorPosition = leftHandCursor.position;

            // Move the cursor
            leftHandCursor.position = cursorPosition + offset * sensitivity;
            // set the rotation based on the vector from shoulder to wrist on the XZ plane
            Vector3 shoulderToWrist = leftWrist.position - leftShoulder.position;

            leftHandCursor.rotation = Quaternion.LookRotation(shoulderToWrist);
        }
    }




    void OnAnimatorIK()
    {
        if (rightHandAnimation_Active)
        {
            IKRightHand();
        }

        if (leftHandAnimation_Active)
        {
            IKLeftHand();
        }
    }

    private void IKRightHand()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, animationPositionWeight_right);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, animationRotationWeight_right);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandCursor.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandCursor.rotation);

        if (rightHandGrabbing)
        {
            //get distance from cursor to shoulder
            float distanceToShoulder = Vector3.Distance(rightHandCursor.position, rightShoulder.position);
            if (distanceToShoulder > armLength)
            {
                Vector3 cursorToShoulder = rightShoulder.position - rightHandCursor.position;
                cursorToShoulder.Normalize();
                rightHandCursor.position = rightShoulder.position - cursorToShoulder * armLength;
            }

        }
    }

    private void IKLeftHand()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, animationPositionWeight_left);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, animationRotationWeight_left);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandCursor.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandCursor.rotation);

        if (leftHandGrabbing)
        {
            //get distance from cursor to shoulder
            float distanceToShoulder = Vector3.Distance(leftHandCursor.position, leftShoulder.position);
            if (distanceToShoulder > armLength)
            {
                Vector3 cursorToShoulder = leftShoulder.position - leftHandCursor.position;
                cursorToShoulder.Normalize();
                leftHandCursor.position = leftShoulder.position - cursorToShoulder * armLength;
            }
        }
    }
}
