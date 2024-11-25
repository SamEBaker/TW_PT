 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegDoorUnlock : MonoBehaviour
{
    public GameObject LockedDoor;
    public GameObject openPos;
    public AudioSource audio;
    public AudioClip UnlockDoor;
    public void OnUnlocked()
    {
        audio.clip = UnlockDoor;
        audio.Play();
        LockedDoor.transform.position = openPos.transform.position;
    }
    
} 
    