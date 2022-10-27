using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class Actor : MonoBehaviour
{
    
    [SerializeField] bool startControllingOnAwake;

    private void Awake()
    {
        if (startControllingOnAwake)
        {

            PlayerController.MainPlayer.ControlActor(this);
        }
    }

    public void BindInputActions(PlayerInput inputTarget)
    {
        foreach (var inputListener in GetComponents<IInputListener>())
        {

            foreach (Action<CallbackContext> function in inputListener.ListenerFunctions)
            {
                inputTarget.onActionTriggered += function;
            }

        }
    }
}
