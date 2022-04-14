using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class HandsController : MonoBehaviour
{
    [SerializeField] private InputControlller _inputControlller;
    [SerializeField] private Hand leftHand;
    [SerializeField] private Hand rightHand;


    private float holdingTime;

    [Header("Scriptable value")] [SerializeField] 
    private HandControllerScriptableObject config;

    

    private void OnEnable()
    {
        _inputControlller.OnMouseClickExited += ResetHoldingValue;
    }

    private void ResetHoldingValue()
    {
        holdingTime = 0;
        SetPunchableScale(true);
    }

    private void SetPunchableScale(bool a)
    {
        leftHand.isPunchScaleable = a;
        rightHand.isPunchScaleable = a;
    }

    private void OnDisable()
    {
        _inputControlller.OnMouseClickExited -= ResetHoldingValue;
    }

    void Update()
    {
        MoveDishes(_inputControlller.DistanceX);
    }

    private void MoveDishes(float distanceX)
    {

        holdingTime += Time.deltaTime;
        if (holdingTime <= config.swipeingTime) return;
        

        if (distanceX > config.DeltaPassingLimit)
        {
            MoveLeftHand();
        }
        else if (distanceX < -config.DeltaPassingLimit)
        {
            MoveRightHand();
        }

        holdingTime = 0;
    }

    private void MoveRightHand()
    {
        SetPunchableScale(false);
        leftHand.PushItem();
    }

    private void MoveLeftHand()
    {
        SetPunchableScale(false);
        rightHand.PushItem();
    }
}