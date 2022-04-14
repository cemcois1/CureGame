using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger 
{
    private static bool IsAnalyticsDebuggable=true;
    public static void LogForAnalytics(string message)
    {
        if (IsAnalyticsDebuggable)
        {
            Debug.Log($"<color=yellow>{message}</color>");
        }
    }
}
