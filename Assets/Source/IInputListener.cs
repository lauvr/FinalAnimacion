using System;
using UnityEngine.InputSystem;

public interface IInputListener 
{
    Action<InputAction.CallbackContext>[] ListenerFunctions { get; }
}
