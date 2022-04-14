using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    private bool isRunnerStarted = false;

    private void OnEnable()
    {
        transform.position = target.position + offset *2;
        GameManager.LevelStarted+=LevelStarted;
        GameManager.RunnerFinished+=LevelFinished;
        GameManager.LevelFailed += LevelFinished;
    }
    private void OnDisable()
    {
        GameManager.LevelStarted-=LevelStarted;
        GameManager.RunnerFinished-=LevelFinished;
        GameManager.LevelFailed -= LevelFinished;
    }
    private void LevelFinished()
    {
        isRunnerStarted = false;
    }

    private void LevelStarted()
    {
        isRunnerStarted = true;
        if (gameObject.name.Contains("vagon")&&!gameObject.name.Equals("vagon"))
        {
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (isRunnerStarted)
        {
            transform.position = Vector3.Lerp(transform.position, target.position+offset, Time.deltaTime*8f);
            transform.LookAt(target.position,Vector3.up);
        }
    }
    
}
