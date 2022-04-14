using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(-1)]
public class ProgressBarStartPositionUpdater : MonoBehaviour
{
    private void OnEnable()
    {
        UIManager.instance.characterTransform = transform;
    }
} 
