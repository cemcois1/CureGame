using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectibleItem : MonoBehaviour, ICollectible
{
    [SerializeField] private int point = 1;

    public int Point
    {
        get => point;
        set => point = value;
    }

    public void Collect()
    {
        if (TryGetComponent(out MeshRenderer aa))
        {
            aa.material.DOColor(Color.white, 1f).From();
        }
    }
}