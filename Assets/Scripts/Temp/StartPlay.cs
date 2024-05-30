using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPlay : MonoBehaviour
{
    public void StartPlaying() {
        SceneManager.LoadScene(Levels.Level0.ToString());
    }
}
