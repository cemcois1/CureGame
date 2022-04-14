using System;
using System.Collections;
using System.Collections.Generic;
#if GameAnalyticsSDK
using GameAnalyticsSDK;
//using GameAnalyticsSDK;
using UnityEngine;

public class GameAnalyticsIitializer : MonoBehaviour
{
    public static GameAnalyticsIitializer instance;
     
    private void Awake()
    {
        GameAnalytics.Initialize();
        instance = this;
        
        DontDestroyOnLoad(this.gameObject);
    }
}
#endif