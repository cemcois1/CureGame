using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private GameObject[] orders;
    [SerializeField] private Sprite[] sprites;

    private void OnEnable()
    {
        GameManager.LevelStarted += OnLevelStarted;
    }
    private void OnDisable()
    {
        GameManager.LevelStarted -= OnLevelStarted;
    }
    private void OnLevelStarted()
    {
        for (int i = 0; i < orders.Length; i++)
        {
            if (orders[i].activeSelf)
            {
                UIManager.instance.ReferanceIcon.sprite = sprites[i];
            }
        }
    }


}