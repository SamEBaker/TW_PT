using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitDoorBehavior : MonoBehaviour
{
    public UnityEvent leavegame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Find the player GameObject and get the PlayerController component
            Player playerController = other.gameObject.GetComponent<Player>();
            //check if that deathroom's player is dead 
            leavegame.Invoke();
            playerController.LeaveGame();
        }
    }


    /*
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            Player playerController = collision.gameObject.GetComponent<Player>();
            playerController.ExitUI.SetActive(false);
        }
    } 
    */
}
