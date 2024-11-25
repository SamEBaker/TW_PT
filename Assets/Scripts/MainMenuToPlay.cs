using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuToPlay : MonoBehaviour
{
    public GameObject howtoPlayScreen;
    [SerializeField]
    private float WaitTime;
    public List<GameObject> screens;

    public EventSystem MainUI;

    public GameObject BackButton;
    public GameObject PlayButton;
    public GameObject ControlScreen;
    public AudioSource _UIaudio;
    public GameObject loadingIcon;
    public GameObject Skip;


    private void Start()
    {
        _UIaudio = GetComponent<AudioSource>();
        MainUI.SetSelectedGameObject(PlayButton);
        ResetScreens();

    }
    public void ResetScreens()
    {
        MainUI.SetSelectedGameObject(PlayButton);
        for (int i = 0; i < screens.Count; i++)
        {
            screens[i].SetActive(false);
        }
        ControlScreen.SetActive(false);

    }
    public void Onplay()
    {
        _UIaudio.Play();
        StartCoroutine(Tutorial());
    }
    public void ToControls()
    {
        _UIaudio.Play();
        //ResetScreens();
        ControlScreen.SetActive(true);

        MainUI.SetSelectedGameObject(BackButton);
    }
    public void OnExit()
    {
        _UIaudio.Play();

        Application.Quit();
    }

    public void ToGame()
    {
        _UIaudio.Play();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator Tutorial()
    {
        MainUI.SetSelectedGameObject(Skip);
        loadingIcon.SetActive(true);
        for(int i = 0; i <= screens.Count - 2; i++)
        {
            screens[i].SetActive(true);
            yield return new WaitForSeconds(WaitTime);
            screens[i].SetActive(false);
        }
        screens[4].SetActive(true);
        yield return new WaitForSeconds(WaitTime);
        ToGame();

        //TutAgainButton.SetActive(true);
        //BackButton.SetActive(false);
    }
}
 