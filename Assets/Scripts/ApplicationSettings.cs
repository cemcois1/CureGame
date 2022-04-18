using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationSettings : MonoBehaviour
{
    [Header("Variables")] [SerializeField] private int frameRate = 60;


    private void Awake()
    {
        QualitySettings.antiAliasing = 0;
        QualitySettings.vSyncCount = 0;
        QualitySettings.SetQualityLevel(0);
        Application.targetFrameRate = frameRate;
    }
}