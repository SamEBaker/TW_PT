using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public static class GameData
{
    public static int FinalGold;
}
public class GameManager : MonoBehaviour
{
    public int TotalGold = 0;
    public int teamOneGold = 0;
    public int teamTwoGold = 0;
    public int playersLeft = 0;
    public int totalPlayers = 0;
    public int totalReady = 0;
    public UnityEvent startGame;
    public GameObject startgamebutton;
    public List<GameObject> PlayerUISetUp;
    public bool[] PlayerReady;
    public List<TMP_Text> TextReady;
    public Timer t;
    public GameObject DefaultCam;
    public GameObject ThirdFiller;
    public GameObject WinDisplay;
    public TMP_Text WinText;
    public GameObject OverlayScreen;

    public Spawner _pool;
    private AudioSource audio;
    public AudioClip UIclick;
    public AudioClip PickupGold;
    public AudioClip EndGameLoseBG;
    public AudioClip LoseRockCrash1;
    public AudioClip LoseRockCrash2;
    public AudioClip EndGameBG;
    public AudioClip EndCoin;

    public void Start()
    {
        bool[] PlayerReady = new bool[3];
        playersLeft = 0;
        _pool = GetComponent<Spawner>();
        audio = GetComponent<AudioSource>();
    }

    public void TotalAddGold(int gold)
    {
        TotalGold += gold;
    }
    public void SpendGold(int gold)
    {
        if(TotalGold != 0)
        {
            audio.clip = PickupGold;
            audio.Play();
            TotalGold -= gold;
        }

    }
    public void SetupUI(int index)
    {
        PlayerUISetUp[index - 1].SetActive(true);
    }
    public void AddPlayer()
    {
        if (totalPlayers == 3)
        {
            ThirdFiller.SetActive(false);
        }
        if (totalPlayers == 2)
        {
            ThirdFiller.SetActive(true);
        }
        if (totalPlayers == 0)
        {
            DefaultCam.SetActive(false);
            //enable startgame button for 1st player
            startgamebutton.SetActive(true);
        }
        totalPlayers++;


        //SetupUI(totalPlayers);
    }
    public void ReadyUp(int index)
    {
        audio.clip = UIclick;
        audio.Play();
        PlayerReady[index] = !PlayerReady[index];
        if (PlayerReady[index] == true)
        {
            TextReady[index].text = "READY!";
            totalReady++;
        }
        else if (PlayerReady[index] == false)
        {
            TextReady[index].text = "Ready?";
            totalReady--;
        }

    }

    public void StartGame()
    {
        if (totalReady >= totalPlayers)
        {
            OverlayScreen.SetActive(true);
            t.IsRunning = true;
            for (int i = 0; i <= 3; i++)
            {
                PlayerUISetUp[i].SetActive(false);
            }
        }

    }
    public void PlayerExited()
    {
       playersLeft++;
       if (playersLeft == totalPlayers)
       {
           EndGame();
       }
    }
    public void EndGame()
    {
        audio.clip = EndCoin;
        audio.Play();
        t.IsRunning = false;
        WinDisplay.SetActive(true);
        WinText.text = "You Found " + TotalGold + " Gold!";
        GameData.FinalGold = TotalGold;
    }
    public void EndGameDied()
    {
        audio.clip = EndGameLoseBG;
        audio.Play();
        audio.clip = LoseRockCrash1;
        audio.Play();
        audio.clip = LoseRockCrash2;
        audio.Play();
        t.IsRunning = false;
        WinDisplay.SetActive(true);
        WinText.text = "You all died";
        GameData.FinalGold = TotalGold;
    }
    public void ToMainMenu()
    {
        audio.clip = UIclick;
        audio.Play();
        SceneManager.LoadScene("MainMenu");
    }
}
