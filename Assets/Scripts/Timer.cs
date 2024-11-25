 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

//https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#:~:text=Making%20a%20countdown%20timer%20in%20Unity%20involves%20storing%20a%20float#:~:text=Making%20a%20countdown%20timer%20in%20Unity%20involves%20storing%20a%20float
public class Timer : MonoBehaviour
{
    public UnityEvent GameOver;
    public float timeRemaining;
    public float TimePickupValue;
    public float maxTime = 120;
    public bool IsRunning = false; 
    public TMP_Text timerText1;
    public TMP_Text timerText2;
    public TMP_Text timerText3;
    public TMP_Text timerText4;
    public TMP_Text timerText5Overlay;
    public int GearsRecieved;
    public AudioSource audio;
    public AudioClip WarningRock1;
    public AudioClip WarningRock2;
    public AudioClip WarningTicking;
    public AudioClip cranking;
    

    void Start()
    {
        //IsRunning = true;
    } 
    void Update()
    {
        if (IsRunning == true)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                Displaytime(timeRemaining);
                if (timeRemaining <= 30)
                {
                    audio.clip = WarningRock1;
                    audio.Play();
                    audio.clip = WarningRock2;
                    audio.Play();
                    audio.clip = WarningTicking;
                    audio.Play();
                    timeRemaining -= Time.deltaTime;
                    Displaytime(timeRemaining);
                }



            }
            else
            {
                timeRemaining = 0;
                IsRunning = false;
                GameOver.Invoke();
            }
        }
        else
        {
            //GameOver.Invoke();
        }

        
    }
    public void StartTime()
    {
        IsRunning = true;
    }

    public void EndTime()
    {
        IsRunning = false; 
        //store timetremaining to gamemanager;
    }
    void Displaytime(float timeDisplay)
    {
        timeDisplay += 1;
        float min = Mathf.FloorToInt(timeDisplay / 60);
        float sec = Mathf.FloorToInt(timeDisplay % 60);

        timerText1.text = string.Format("{0:00}:{1:00}", min, sec);
        timerText2.text = string.Format("{0:00}:{1:00}", min, sec);
        timerText3.text = string.Format("{0:00}:{1:00}", min, sec);
        timerText4.text = string.Format("{0:00}:{1:00}", min, sec);
        timerText5Overlay.text = string.Format("{0:00}:{1:00}", min, sec);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the player GameObject and get the PlayerController component
            Player playerController = other.GetComponent<Player>();
            if(playerController.Gear != 0)
            {
                playerController.UseItem();
                AddTime();
            }
            else
            {
                //call player hud flashgear
            }
        }
    }

    public void AddTime()
    {
        audio.clip = cranking;
        audio.Play();
        timeRemaining += TimePickupValue;
        GearsRecieved += 1;
        //cap timer at 160 add if timer is at full - 10 seconds to not take a gear
        //timeRemaining = Mathf.Min(timeRemaining, maxTime);
    }
}
