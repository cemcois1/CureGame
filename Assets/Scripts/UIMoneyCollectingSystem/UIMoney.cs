using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Developing.Scripts.UIMoneyCollectingSystem
{
    public class UIMoney : MonoBehaviour
    {
        [SerializeField] RectTransform rectTransform;

        private void OnEnable()
        {
            rectTransform = transform.parent.GetComponent<RectTransform>();
            CollectMoney();
        }

        private void CollectMoney()
        {
            transform.DOMove(rectTransform.position, .3f).SetDelay(Random.Range(0f, .5f))
                .OnComplete(() => Destroy(gameObject));
        }
    }
}