 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    public int KeyType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            // Find the player GameObject and get the PlayerController component
            Player playerController = other.GetComponent<Player>();

            if (playerController != null)
            {
                if (KeyType == 0)
                {
                    playerController.GetKey(0);
                    Destroy(this.gameObject);
                }
                else if (KeyType == 1)
                {
                    playerController.GetKey(1);
                    Destroy(this.gameObject);
                }
                else if (KeyType == 2)
                {
                    playerController.GetKey(2); 
                    Destroy(this.gameObject);
                }
                else if (KeyType == 3)
                {
                    playerController.GetKey(3);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
