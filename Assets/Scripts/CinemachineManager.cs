using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CinemachineManager : MonoBehaviour
{
    [FormerlySerializedAs("_virtualCamera")] [SerializeField] private CinemachineVirtualCamera virtualGameplayCamera;
    [FormerlySerializedAs("_virtualCamera")] [SerializeField] private CinemachineVirtualCamera virtualMiniGameCamera;
    private CinemachineTransposer gameplayCinemachineTransposer;
    private CinemachineTrackedDolly minigameCinemachineTrackedDolly;
    private CinemachineImpulseSource _impulseSource;
    
    public static Action<float> ChangeFollowOffset;
    public static Action ShakeScreen;
    private void OnEnable()
    {
        _impulseSource=GetComponent<CinemachineImpulseSource>();
        gameplayCinemachineTransposer = virtualGameplayCamera.GetCinemachineComponent<CinemachineTransposer>();
        minigameCinemachineTrackedDolly = virtualMiniGameCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        
        ChangeFollowOffset += ChangeOffset;
        ShakeScreen += OnShakeScreen;
        GameManager.LoadLevelEndCamera+=CloseGamePlayCamera;
        GameManager.LoadLevelEndCamera+=LoadMiniLevel;
    }
    private void OnDisable()
    {
        ChangeFollowOffset -= ChangeOffset;
        ShakeScreen-= OnShakeScreen;
        GameManager.LoadLevelEndCamera-=CloseGamePlayCamera;
        GameManager.LoadLevelEndCamera-=LoadMiniLevel;
    }
    private void LoadMiniLevel()
    {
        virtualMiniGameCamera.gameObject.SetActive(true);
        DOTween.To(() => minigameCinemachineTrackedDolly.m_PathPosition,
            (value) => minigameCinemachineTrackedDolly.m_PathPosition = value, 1f, 1f).SetEase(Ease.Linear).SetDelay(.5f);
    }

    private void CloseGamePlayCamera()
    {
        virtualGameplayCamera.gameObject.SetActive(false);
        
    }



    private void ChangeOffset(float weight)
    {
        var finalValue = gameplayCinemachineTransposer.m_FollowOffset.z +weight;
        DOTween.To(() => gameplayCinemachineTransposer.m_FollowOffset,
            (value) => gameplayCinemachineTransposer.m_FollowOffset = value, 
            new Vector3(gameplayCinemachineTransposer.m_FollowOffset.x, gameplayCinemachineTransposer.m_FollowOffset.y, finalValue),
            1f);
    }

    public  void OnShakeScreen()
    {
        _impulseSource.GenerateImpulse(.5f);
    }
}