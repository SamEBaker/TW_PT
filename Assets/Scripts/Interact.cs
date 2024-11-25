using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interact : MonoBehaviour
{
    public string popUpMessage;
    public IEnumerator flashtext(Player playercontroller)
    {
        playercontroller.InteractPopUp(popUpMessage);
        yield return new WaitForSeconds(3f);
        playercontroller.InteractPopUp(" ");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            // Find the player GameObject and get the PlayerController component
            Player playerController = other.GetComponent<Player>();
            StartCoroutine(flashtext(playerController));
            //playerController.InteractPopUp(popUpMessage);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Player playerController = other.GetComponent<Player>();
            //playerController.InteractPopUp(" ");
        }

    }
}
