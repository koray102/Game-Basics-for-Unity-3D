using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider slider;
    public GameObject loadScene;
    public GameObject mainMenu;
    public void LoadTheScene(string Scene)
    {
        //SceneManager.LoadScene(Scene);
        StartCoroutine(loadAsync(Scene));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator loadAsync(string Scene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(Scene);
        loadScene.SetActive(true);
        
        mainMenu.SetActive(false);

        while(!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / .9f);

            Debug.Log(progress);
            slider.value = progress;
            yield return null;
        }
    }
}
