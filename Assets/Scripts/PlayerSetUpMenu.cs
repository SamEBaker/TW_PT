 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//https://www.youtube.com/watch?v=_5pOiYHJgl0
public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private Button readyButton;
    public GameObject menu;
    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;
    public void setPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    // Update is called once per frame 
    void Update()
    {
        if (Time.time > ignoreInputTime)
        { 
            inputEnabled = true;
        }
    }


    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }

        readyButton.gameObject.SetActive(false);
        //menu.SetActive(false);
    }
}

