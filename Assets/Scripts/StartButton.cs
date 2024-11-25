 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
        Scene scene;

        void Start()
        {
            scene = SceneManager.GetActiveScene();

        }

        void OnGUI()
        {
            GUI.skin.button.fontSize = 20;

            if (GUI.Button(new Rect(10, 80, 180, 60), "Change from scene " + scene.buildIndex))
            {
                int nextSceneIndex = Random.Range(0, 4);
                SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
            }
        } 
    }
 