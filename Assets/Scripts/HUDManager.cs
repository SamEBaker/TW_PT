using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TMP_Text PopUp;
    public TMP_Text ErrorPop;
    public TMP_Text Gold;
    public TMP_Text Ammo;
    public GameManager gm;
    public List<GameObject> Keys;
    //public GameObject keyUI;
    public GameObject deathScreen;
    public TMP_Text UpdateText;
    public TMP_Text Gears;
    public TMP_Text PHealth;
    public Player player;
    public GameObject DeathScreen;
    public GameObject menu;
    //public GameObject readyText;

    // Update is called once per frame
    void Update()
    {
        DisplayplayerHealth();
        DisplayGold();
        DisplayAmmo();
    }
     
    public void DisplayKey(int index)
    {
        Keys[index].SetActive(true);
        //set key false when used
    }
    public void SetKeyOff(int index)
    {
        Keys[index].SetActive(false);
    }

    public void SetLoseSetPlayerDeathScreen()
    {
        deathScreen.SetActive(true);
    }
    public void InteractPopUp(string popUpMessage)
    {

        PopUp.text = popUpMessage; ;
    }
    public void InteractPopDown()
    {
        PopUp.text = "";
        //PopUp.SetActive (false);
    }

    public void ShowMessage(string message)
    {

        ErrorPop.text = message;
    }
    public void DropMessage()
    {
        ErrorPop.text = "";
    }

    public void DisplayGold()
    {
        Gold.text = gm.TotalGold.ToString();
    }
    public void DisplayAmmo()
    {
        Ammo.text = "Ammo: " + player.playerAmmo;
    }
    public void DisplayGears()
    {
        Gears.text = player.Gear.ToString();
    }

    public void setmenu()
    {
        menu.SetActive(false);
    }
    IEnumerator FlashText(TMP_Text text)
    {
        text.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        text.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        text.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        text.color = Color.white;
    }
    public void FlashGearError()
    {
        //play sound
        StartCoroutine(FlashText(Gears)); 
    }
    public void FlashAmmoError()
    {
        //play sound
        StartCoroutine(FlashText(Ammo));
    }
    public void DisplayDeath()
    {
        DeathScreen.SetActive(true);
    }
    public void DisplayDisableDeath()
    {
        DeathScreen.SetActive(false);
    }
    public void DisplayplayerHealth()
    {
        PHealth.text = "Health: " + player.playerHealth;
    }
}
