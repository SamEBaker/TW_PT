using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour
{
    HUDManager HUDM;
    //public Player player;
    public bool Locked;
    public GameObject Door;
    public GameObject DoorPos;

     /*
    public void OpenDoor()
    {
        if(Locked == true)
        {
            if(player.RedKey == true)
            {
                Locked = false;
                Debug.Log("Entering next area");
                Door.transform.position = DoorPos.transform.position;


            } 
            else
            {

                //show hud message Door is locked
                Debug.Log("Door is locked!");
            }
        }
        else 
        {
            Debug.Log("Entering next area");
            Door.transform.position = DoorPos.transform.position;
        }


        

    }
    public void OpenEndGameDoor()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    */
     //

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {

            Locked = false;
            Debug.Log("Entering next area");
            Door.transform.position = DoorPos.transform.position;
        }
        else
        {
            Debug.Log("Door is locked!");
        }

    }

}
