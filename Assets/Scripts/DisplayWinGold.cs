using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DisplayWinGold : MonoBehaviour
{
    public GameManager gm;
    public TMP_Text text;
    //display individual and total gold/deaths

    public void GoBackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Update()
    {
        text.text = "You collected " + gm.TotalGold + "  gold!";
    }

}
 