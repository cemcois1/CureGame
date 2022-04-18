using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField] private AudioSource _stateFinishedSound;
    [SerializeField] private AudioSource _ItemSellectedSound;
    [SerializeField] private AudioSource _TalkSound;


    public void ItemSellectedSound()
    {
        _ItemSellectedSound.Play();
    }

    public void TalkSound()
    {
        _TalkSound.Play();
    }

    public void stateFinishedSound()
    {
        _stateFinishedSound.Play();
    }
}