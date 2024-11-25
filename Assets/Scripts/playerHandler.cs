 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class playerHandler : MonoBehaviour
{
    private Player player;
    private PlayerInput playerInput;
    //public CameraFollow cam;

    private void Awake()
    { 
        var players = FindObjectsOfType<Player>();
        playerInput = GetComponent<PlayerInput>(); 
        var index = playerInput.playerIndex;
        player = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
        playerInput.camera = player.myCam;
        playerInput.uiInputModule = player.m;
        //cam.target = player.transform;
    }
    public void Move(InputAction.CallbackContext ctx)
    { 
        player.OnMove(ctx);
    }
    public void Rotate(InputAction.CallbackContext ctx) 
    {
        player.OnRotate(ctx);
    }
    public void OnDrop()
    {
        player.Drop();
    }
    public void Sprint()
    {
        player.OnSprint();
    }
    public void Shoot()
    {
        player.OnShoot();
    }
}
