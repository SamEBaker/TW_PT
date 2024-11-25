using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{ 
    public GameObject VaultDoors;
    public GameObject VaultDoorsPos;
    public AudioSource audio;
    public AudioClip UnlockedDoor;
    public int KeyType;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Find the player GameObject and get the PlayerController component
            Player playerController = collision.gameObject.GetComponent<Player>();
            if (playerController != null)
            {
                if (KeyType == 0)
                {
                    if (playerController.HasKey[0] == true)
                    {
                        OnUnlocked();
                        playerController.LoseKey(0);
                    }
                }
                else if (KeyType == 1)  
                { 
                    if (playerController.HasKey[1] == true)
                    { 
                        OnUnlocked();
                        playerController.LoseKey(1);

                    }
                }
                else if (KeyType == 2)
                {
                    if (playerController.HasKey[2] == true)
                    {
                        OnUnlocked();
                        playerController.LoseKey(2);

                    }
                }
                else if (KeyType == 3)
                {
                    if (playerController.HasKey[3] == true)
                    {
                        OnUnlocked();
                        playerController.LoseKey(3);

                    }
                }
            }
        }
    }

    public void OnUnlocked()
    {
        audio.clip = UnlockedDoor;
        audio.Play();
        VaultDoors.transform.position = VaultDoorsPos.transform.position;
    }

}
