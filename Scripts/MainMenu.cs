using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    AsyncOperation loadScene;
    public GameObject loadingScreen;
    public bool startLoad;
    public Text percenText;
    public void startGame()
    {
        //SceneManager.LoadScene(1);
        loadScene = SceneManager.LoadSceneAsync(1);
        loadingScreen.SetActive(true);
        startLoad = true;
    }

    public void quitGame()
    {
        //Debug.Log("Quit game");
        Application.Quit();
    }

    public void helpMenu()
    {
        //Debug.Log("Help Menu");
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
    }

    private void Update()
    {
        if (startLoad) //used for loading screen
        {
            float progressValue = Mathf.Clamp01(loadScene.progress / 0.9f);

            percenText.text = Mathf.Round(progressValue * 100) + "%";
        }
    }
}
