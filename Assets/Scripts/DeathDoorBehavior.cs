using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDoorBehavior : MonoBehaviour
{
    public int PlayerIDoor;
    public GameObject deadplayer;
    public GameObject spawnpoint;
     
    public void Unlock()
    {
        deadplayer.transform.position = spawnpoint.transform.position;
    }

}
 