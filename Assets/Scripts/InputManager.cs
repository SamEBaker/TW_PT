using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class MoveInputEvent : UnityEvent<float, float> {  }
[Serializable]
public class DropInputEvent : UnityEvent { }

[Serializable]
public class SprintInputEvent : UnityEvent { }

[Serializable]
public class ShootInputEvent : UnityEvent { }

[Serializable]
public class PlaceInputEvent : UnityEvent { }
public class InputManager : MonoBehaviour
{
    PlayerControls pC;
   public MoveInputEvent moveInputEvent;
    public DropInputEvent dropInputEvent; 
    public SprintInputEvent sprintInputEvent;
    public ShootInputEvent shootInputEvent;
    public PlaceInputEvent InteractInputEvent;

    public  Player player;

    private void Awake()
    {
        pC = new PlayerControls();
    }

    private void OnEnable()
    {
        pC.Gameplay.Enable();
        pC.Gameplay.Move.performed += OnMovePerformed;
        pC.Gameplay.Move.canceled += OnMovePerformed;
        pC.Gameplay.Drop.performed += OnDropPerformed;
        pC.Gameplay.Sprint.performed += OnSprintPerformed;
        pC.Gameplay.Shoot.performed += OnShootPerformed;
        pC.Gameplay.Select.performed += OnInteractPerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 moveInput = ctx.ReadValue<Vector2>();
        moveInputEvent.Invoke(moveInput.x, moveInput.y);

    }
    private void OnDropPerformed(InputAction.CallbackContext ctx)
    {
        dropInputEvent.Invoke();
    }
    private void OnSprintPerformed(InputAction.CallbackContext ctx)
    {
        sprintInputEvent.Invoke();
    }
    private void OnShootPerformed(InputAction.CallbackContext ctx)
    {
        shootInputEvent.Invoke();
    }

    private void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
       InteractInputEvent.Invoke();
    }

}
