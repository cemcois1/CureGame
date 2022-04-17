using System;
using System.Collections;
using System.Collections.Generic;
using Developing.Scripts.CureGame;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager instance;
    public int MoneyCount = 150;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    [Space] [Header("UI Variables")] [SerializeField]
    public Transform characterTransform;

    [SerializeField] private GameObject Tap2Play;
    [SerializeField] private GameObject Done;
    [SerializeField] private GameObject SellectObjectsText;
    [SerializeField] private GameObject HoldAndDragText;

    [SerializeField] private GameObject LevelFailed;
    [SerializeField] private GameObject LevelCompleted;

    private bool _isGameOver;

    private void OnEnable()
    {
        GameManager.PrepareLevel += PrepareLevelUI;
        GameManager.LevelStarted += OnLevelStarted;
        GameManager.LevelFailed += OnLevelFailed;
        GameManager.RunnerFinished += OnLevelEnded;
        GameManager.MinilevelFinished += LevelComplatedUI;
    }


    private void OnDisable()
    {
        GameManager.PrepareLevel -= PrepareLevelUI;
        GameManager.LevelStarted -= OnLevelStarted;
        GameManager.LevelFailed -= OnLevelFailed;
        GameManager.RunnerFinished -= OnLevelEnded;
        GameManager.MinilevelFinished -= LevelComplatedUI;
    }

    private void PrepareLevelUI()
    {
        LevelCompleted.SetActive(false);
        LevelFailed.SetActive(false);
        Tap2Play.SetActive(true);
        _isGameOver = false;
    }


    public void OnLevelStarted()
    {
        Tap2Play.SetActive(false);
    }


    public void OnLevelFailed()
    {
        LevelFailed.SetActive(true);
    }

    public void OnLevelEnded()
    {
        _isGameOver = true;
    }

    
    public void GoNextPart()
    {
        PartManager.PartComplated?.Invoke();
        Done.SetActive(false);
    }

    public void OpenDoneButton()
    {
        Done.SetActive(true);
    }

    public void OpenSellectObjectsText()
    {
        print("open");
        SellectObjectsText.SetActive(true);
    }

    private void CloseSellectObjectsText()
    {
        SellectObjectsText.SetActive(false);
        InputControlller.OnMouseClicked -= CloseSellectObjectsText;
    }
    
    #region Event Methods

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.PrepareLevel?.Invoke();
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.PrepareLevel?.Invoke();
    }

    public void LevelComplatedUI()
    {
        StartCoroutine(StaticMethods.MakeActionWithDelay(() => LevelCompleted.SetActive(true), 2f));
    }

    #endregion

}