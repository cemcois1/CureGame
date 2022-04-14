using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static Action LevelStarted;
    public static Action RunnerFinished;
    public static Action LevelFailed;
    public static Action PrepareLevel; //todo load New level or 
    public static Action LoadMinilevel;
    public static Action MinilevelFinished;
    public static Action LoadLevelEndCamera;
    public int lastSceneIndex = -1;
    public int attemptNumber = 1;

    private void Awake()
    {
        #region Singleton

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            //Logger.LogForAnalytics("GameStart();");
            //GameAnalytics.StartSession();
        }
        else
        {
            Destroy(this.gameObject);
        }

        #endregion
    }

    private void OnEnable()
    {
        LevelStarted += OnLevelStarted;
        LevelStarted += StartRunning;
        RunnerFinished += UpdateLastSceneIndex;
        LevelFailed += IncreaseAttempNumber;
        LevelFailed += AddRetryLionAnalytics;
        MinilevelFinished += AddWinToLion;
    }

    private void UIManagerOnOnPressedRetryButton()
    {
        Logger.LogForAnalytics("LevelRestart()");
        
    }


    private void OnDisable()
    {
        LevelStarted -= OnLevelStarted;
        LevelStarted -= StartRunning;
        RunnerFinished -= UpdateLastSceneIndex;
        LevelFailed -= IncreaseAttempNumber;
        LevelFailed -= AddRetryLionAnalytics;

        MinilevelFinished -= AddWinToLion;
    }

    private void IncreaseAttempNumber()
    {
        attemptNumber++;
    }

    private void AddRetryLionAnalytics()
    {
        Logger.LogForAnalytics("");//LevelFail(PlayerPrefs.GetInt("LevelCount") + 1, attemptNumber)

    }

    private void AddWinToLion()
    {

        //LevelComplete(PlayerPrefs.GetInt("LevelCount") + 1, attemptNumber);
        //NewProgressionEvent(GAProgressionStatus.Complete, "Level -" + (PlayerPrefs.GetInt("LevelCount") + 1));
        ResetAttempNumber();
    }

    private void ResetAttempNumber()
    {
        print("attemptNumber resetlendi 1");
        attemptNumber = 1;
    }

    private void UpdateLastSceneIndex()
    {
        lastSceneIndex = PlayerPrefs.GetInt("LevelCount");
    }


    void OnLevelStarted()
    {
        if (lastSceneIndex == PlayerPrefs.GetInt("LevelCount"))
        {
            if (attemptNumber == 1)
            {
                print("Leveli  oynuyorsun");
            }
            else
            {
                print("Leveli  TEKRAR   oynuyorsun");
            }

            //Logger.LogForAnalytics("LionAnalytics.LevelStart");
            //LevelStart(PlayerPrefs.GetInt("LevelCount") + 1, attemptNumber);
            IncreaseAttempNumber();
        }
        else
        {
            //Logger.LogForAnalytics("LionAnalytics.LevelStart");
            //LevelStart(PlayerPrefs.GetInt("LevelCount") + 1, attemptNumber);
            IncreaseAttempNumber();
        }
    }

    void StartRunning()
    {
        print("Running Started");
        //TODO Start Running
    }
}