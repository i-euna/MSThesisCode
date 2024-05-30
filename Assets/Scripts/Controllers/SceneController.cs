using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ReloadCurrentScene() {
        string scene = SceneManager.GetActiveScene().name;
        LoadSceneWithName(scene);
    }

    public static void LoadSceneWithName(string sceneName) {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void GoToHome()
    {
        LoadSceneWithName("Menu");
    }
}
