using System.Collections;
using System.Collections.Generic;
//using LionStudios.Suite.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int childCount;
    private int LevelCount;
    private int startingIndex = 0;
    private void OnEnable()
    {
        childCount = transform.childCount;
        //print("ChildCount=" + childCount);
        startingIndex = LevelCount = PlayerPrefs.GetInt("LevelCount");
        print(startingIndex + "is Starting Level index");
        #region CloseAllScenes
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        #endregion
        OpenLevel();
    }

    private void OpenLevel()
    {
        
        transform.GetChild(LevelCount % childCount).gameObject.SetActive(true);
    }

    public void LoadNextLevel()
    {
        transform.GetChild(LevelCount % childCount).gameObject.SetActive(false);
        PlayerPrefs.SetInt("LevelCount", PlayerPrefs.GetInt("LevelCount") + 1);
        LevelCount= PlayerPrefs.GetInt("LevelCount");
        if (LevelCount==0)
        {
            PlayerPrefs.SetInt("LevelCount", PlayerPrefs.GetInt("LevelCount") + 1);
            LevelCount= PlayerPrefs.GetInt("LevelCount");
        }
        transform.GetChild(LevelCount % childCount).gameObject.SetActive(true);
        print("Starting index" + startingIndex + "LevelCount: " + LevelCount);
        if (startingIndex + childCount -1 == LevelCount)
        {
            print("Load Scene Again");
            SceneManager.LoadScene(0);
        }
        GameManager.PrepareLevel?.Invoke();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
        GameManager.PrepareLevel?.Invoke();
    }

}
