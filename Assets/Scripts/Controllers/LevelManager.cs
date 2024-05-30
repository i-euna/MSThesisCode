using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private IntVariable Health;

    [SerializeField]
    private IntVariable AmmoCount;

    [SerializeField]
    private IntVariable GameTotalKilled, GameTotalAmmo;

    [SerializeField]
    private IntVariable TotalSpawnedEnemy, TotalDestroyedEnemy,
        LevelTotalAmmo, LevelTotalKilled;

    [SerializeField]
    private GameObject LevelFailurePanel, LevelSuccessPanel;

    [SerializeField]
    private Levels CurrentLevel;
    [SerializeField]
    private Levels NextLevel;

    private void Awake()
    {
        CurrentLevelSettings.CurrentLevel = CurrentLevel;
        LevelTotalAmmo.Value = 0;
        TotalSpawnedEnemy.Value = 0;
        TotalDestroyedEnemy.Value = 0;
        LevelTotalKilled.Value = 0;
    }

    public static Levels GetCurrentLevel() {
        return CurrentLevelSettings.CurrentLevel;
    }
    public void CheckGameOver() {

        //if all enemies are killed/destroyed
        //level success

        if (Health.Value == 0 || AmmoCount.Value <= 0)
        {
            HandleLevelFailure();
        }

        if (TotalSpawnedEnemy.Value == TotalDestroyedEnemy.Value 
            && Health.Value != 0) {
            HandleSuccess();
        }

        
    }

    void HandleLevelFailure()
    {
        Time.timeScale = 0;
        LevelFailurePanel.SetActive(true);
    }

    void HandleSuccess() {
        Debug.Log(CurrentLevel + " - Successful");
        //Debug.Log("Loading Next Level " + NextLevel);
        
        LevelSuccessPanel.SetActive(true);

        UpdateAmmoEnemyStat();
        Time.timeScale = 0;
    }

    public void LoadNextLevel() {
        Debug.Log("Loading next ");
        switch (CurrentLevel) {
            case Levels.Level2:
                if (GameTotalKilled.Value >= 25 &&
                    GameTotalAmmo.Value <= 30)
                    NextLevel = Levels.Level3H;
                else NextLevel = Levels.Level3E;
                break;
            case Levels.Level3E:
                if (LevelTotalKilled.Value >= 17 &&
                    LevelTotalAmmo.Value <= 22)
                    NextLevel = Levels.Level4M;
                else NextLevel = Levels.Level4E;
                break;
            case Levels.Level3H:
                if (LevelTotalKilled.Value >= 17 &&
                    LevelTotalAmmo.Value <= 22)
                    NextLevel = Levels.Level4M;
                else NextLevel = Levels.Level4H;
                break;
            default:
                break;
        }
        Debug.Log("Next Level " + NextLevel.ToString());
        SceneController.LoadSceneWithName(NextLevel.ToString());
    }

    void UpdateAmmoEnemyStat() {
        if (CurrentLevel == Levels.Level0) {
            GameTotalKilled.Value = 0;
            GameTotalAmmo.Value = 0;
        }

        GameTotalKilled.Value += LevelTotalKilled.Value;
        GameTotalAmmo.Value += LevelTotalAmmo.Value;
    }


}
