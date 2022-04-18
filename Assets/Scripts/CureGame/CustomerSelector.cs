using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CustomerSelector : MonoBehaviour
{
    #region Singleton

    public static CustomerSelector instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public Animator[] animator;
    [SerializeField] private Transform levelEndTransform;
    [SerializeField] private ParticleSystem[] particles;

    [SerializeField] private GameObject[] customerlist;
    [SerializeField] private int index;

    [ContextMenu("changeCustomerList")]
    public void changeCustomerList()
    {
        for (int i = 0; i < customerlist.Length; i++)
        {
            customerlist[i].SetActive(false);
        }

        print("Worked");
        customerlist[index % customerlist.Length].SetActive(true);
        index++;
    }

    [ContextMenu("ChangePositionAndAnimation")]
    public void ChangePositionAndAnimation()
    {
        for (int i = 0; i < animator.Length; i++)
        {
            if (!animator[i].gameObject.activeSelf) return;
            animator[i].SetTrigger("LevelEndAnimation");
            animator[i].transform.DOMove(levelEndTransform.position, 2f);
        }
    }

    public void PlayHappyParticle()
    {
        particles[0].Play();
        particles[1].Play();
    }

    public void PlaySadParticle()
    {
        particles[2].Play();
    }
}