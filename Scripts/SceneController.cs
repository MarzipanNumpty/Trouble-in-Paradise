using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    AsyncOperation loadScene;
    public GameObject loadingScreen;
    public bool startLoad;
    public Text percenText;
    public void EndGame()
    {
        //SceneManager.LoadScene(0);
        loadScene = SceneManager.LoadSceneAsync(0);
        loadingScreen.SetActive(true);
        Time.timeScale = 1.0f;
        startLoad = true;
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene(1);
        loadScene = SceneManager.LoadSceneAsync(0);
        loadingScreen.SetActive(true);
        Time.timeScale = 1.0f;
        startLoad = true;
    }

    private void Start()
    {
        loadingScreen.SetActive(false);
        startLoad = false;
    }

    private void Update()
    {
        if(startLoad)
        {
            float progressValue = Mathf.Clamp01(loadScene.progress / 0.9f);

            percenText.text = Mathf.Round(progressValue * 100) + "%";
        }
    }


}
