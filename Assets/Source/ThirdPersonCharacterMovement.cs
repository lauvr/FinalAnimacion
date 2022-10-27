using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ThirdPersonCharacterMovement : MonoBehaviour, IInputListener
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float acceleration = 5;
    [SerializeField] private float deceleration = 5;
    [SerializeField] private float angularDampening = 90;

    private Vector2 inputVector; 
    private Vector2 wetInputVector;

    private Vector3 lastMotion; 

   
    public void Move(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    void DampenInput()
    {
        float x = wetInputVector.x;
        float y = wetInputVector.y; ;

        if (inputVector.x == 0)
        {
            x = Mathf.MoveTowards(x, 0f, Time.deltaTime * deceleration);
        }
        else
        {
            x = Mathf.MoveTowards(x, inputVector.x, Time.deltaTime * acceleration);
        }
        //------------------------
        if (inputVector.y == 0)
        {

            y = Mathf.MoveTowards(y, 0f, Time.deltaTime * deceleration);
        }
        else
        {
            y = Mathf.MoveTowards(y, inputVector.y, Time.deltaTime * acceleration);
        }
        wetInputVector.Set(x, y);
    }

    private void Update()
    {
        DampenInput();
        Transform cameraTransform = Camera.main.transform;
        Vector3 right = Vector3.ProjectOnPlane(cameraTransform.right, cameraTransform.up);
        Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, cameraTransform.up);
        Vector3 motionVector = right * (wetInputVector.x * movementSpeed * Time.deltaTime) +
                              forward * (wetInputVector.y * movementSpeed * Time.deltaTime);
        transform.Translate(motionVector, Space.World);

        if (motionVector.magnitude > 0)
        {
            //Slerp= interpolacion esferica
            transform.forward = Vector3.Slerp(lastMotion.normalized, motionVector.normalized, Time.deltaTime * angularDampening);
            //frame1 lastmotion=1 y motion=-1, forward=1/60=0.016 (un valor entre 1 y -1)
            //frame2 lastmotion= parecerse mas al motion poco a poco de forma no linear pues la interpolacion es esferica
        }
        lastMotion = transform.forward;
    }

    public Action<InputAction.CallbackContext>[] ListenerFunctions => new Action<InputAction.CallbackContext>[] { Move };
}
