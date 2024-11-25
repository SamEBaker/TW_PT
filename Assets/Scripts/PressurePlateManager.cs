 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateManager : MonoBehaviour
{
    public UnityEvent puzzleSolved;
    public int PressedBlocks = 0;
    public int PadsNeeded;
    public bool DoorOpened = false; 

    public void ClearAll()
    {
        PressedBlocks = 0;
        DoorOpened = false;
    }
    public void Addpressed() { 
        PressedBlocks++; 
        //play corect sound
    }
    public void Removepressed() { PressedBlocks--;}
    // Update is called once per frame
    void Update()
    {
        if(PressedBlocks == PadsNeeded && DoorOpened == false)
        {
            Debug.Log("Invoke event");
            DoorOpened = true;
            puzzleSolved.Invoke();
        }
    }
}
