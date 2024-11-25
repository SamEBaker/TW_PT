using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PickupType
{
    Ammo,
    Gear,
    Gold,
    Key1,
    Key2,
    Key3,
    Key4
}

public class ItemBehavior : MonoBehaviour
{
    public GameManager gm;
    public UnityEvent doorOpen;
    public float timeValue;
    public int goldValue;
    public int AmmoValue;
    public string popUpMessage;
    public PickupType pickupType;

    public void PickUpGold() 
    {
        Destroy(this.gameObject);
        gm.TotalAddGold(goldValue);
        //hmd.DisplayGold();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            // Find the player GameObject and get the PlayerController component
            Player playerController = other.GetComponent<Player>(); 
            playerController.InteractPopUp(popUpMessage);
            if (playerController != null)
            {
                if(pickupType == PickupType.Ammo)
                {
                    playerController.AddAmmo(AmmoValue);
                    Destroy(this.gameObject );
                }
                else if(pickupType  == PickupType.Gear)
                {
                    playerController.AddGear();
                    Destroy(this.gameObject);
                }
                else if(pickupType == PickupType.Gold)
                {
                    playerController.AddGold(goldValue);
                    Destroy(this.gameObject);
                }
                // playerController. call func needed;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerController = other.GetComponent<Player>();
            //playerController.InteractPopUp(popUpMessage);
            // hmd.InteractPopDown();
            playerController.InteractPopUp("");
        }

    }

    public void PickUpAmmo()
    {
        Destroy(this.gameObject);
        //GetComponent<Player>().AddAmmo(AmmoValue);
        //hudManager.DisplayGold();
    }

    public void PickUpTime()
    {
        Destroy(this.gameObject);
        //GetComponent<Player>().AddGear();
        //time.AddTime(timeValue);
        //update ui inventory
    }
    public void PickUpKey()
    {
        Destroy(this.gameObject);
        //Debug.Log("You Picked this up!");
        //GetComponent<Player>().UnlockDoor();
       // hmd.SetKeyActive();
    }

}
