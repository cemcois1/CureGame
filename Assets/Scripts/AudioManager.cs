using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static Action LevelEndFillingSound;
    public static Action ItemCollectedSound;
    public static Action FinalAudioClipSound;
    public static Action PlayBarierAudioClipSound;

    [SerializeField] private AudioSource _collectSource;
    [SerializeField] private AudioSource _Hitsource;
    [SerializeField] private AudioSource _Runsource;
    [SerializeField] private AudioSource _Minigamesource;
    [SerializeField] private AudioSource BarierAudioSource;

    [FormerlySerializedAs("_MinigameAudioClip")] [SerializeField]
    private AudioClip _minigameFinishedAudioClip;

    [FormerlySerializedAs("_finalAudioClip")] [SerializeField]
    private AudioClip _finalFillingAudioClip;

    [SerializeField] private AudioClip _collectingAudioClip;


    private void OnEnable()
    {
        GameManager.LoadMinilevel += CloseSoundsforMinilevel;
        GameManager.LevelFailed += CloseSoundsforMinilevel;
        GameManager.MinilevelFinished += PlayLevelEndFinishSound;
        GameManager.PrepareLevel += ResetPitchValue;

        CharacterManager.CharacterStateChanged += RockBreakingSoundEffect;
        CharacterManager.CharacterStateChanged += WalkingSound;

        LevelEndFillingSound += PlayLevelEndFillingSound;
        ItemCollectedSound += PlayItemCollectedSound;
        FinalAudioClipSound += PlayFinalAudioClipSound;
        PlayBarierAudioClipSound += BarierAudioClipSound;
    }

    private void OnDisable()
    {
        GameManager.LoadMinilevel -= CloseSoundsforMinilevel;
        GameManager.LevelFailed -= CloseSoundsforMinilevel;
        GameManager.MinilevelFinished -= PlayLevelEndFinishSound;
        GameManager.PrepareLevel -= ResetPitchValue;
        
        CharacterManager.CharacterStateChanged -= RockBreakingSoundEffect;
        CharacterManager.CharacterStateChanged -= WalkingSound;

        LevelEndFillingSound -= PlayLevelEndFillingSound;
        ItemCollectedSound -= PlayItemCollectedSound;
        FinalAudioClipSound -= PlayFinalAudioClipSound;
        PlayBarierAudioClipSound -= BarierAudioClipSound;
    }

    private void BarierAudioClipSound()
    {
        BarierAudioSource.Play();
    }

    private void ResetPitchValue()
    {
        _collectSource.pitch = 1f;
    }

    private void CloseSoundsforMinilevel()
    {
        _Runsource.enabled = false;
    }

    private void WalkingSound(CharacterState state)
    {
        if (state == CharacterState.Running)
        {
            _Runsource.enabled = true;
            _Runsource.Play();
        }
    }

    private void PlayFinalAudioClipSound()
    {
        _collectSource.PlayOneShot(_finalFillingAudioClip);
    }


    private void PlayItemCollectedSound()
    {
        _collectSource.Play();
    }

    private void PlayLevelEndFillingSound()
    {
        _collectSource.pitch = _collectSource.pitch < 2f ? _collectSource.pitch + .1f : 2f;
        _collectSource.Play();
    }

    private void PlayLevelEndFinishSound()
    {
        _collectSource.PlayOneShot(_minigameFinishedAudioClip);
    }

    private void RockBreakingSoundEffect(CharacterState state)
    {
        if (state != CharacterState.Fight) return;
        _Hitsource.Play();
    }
}