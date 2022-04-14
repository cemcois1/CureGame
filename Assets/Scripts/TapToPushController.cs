using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TapToPushController : MonoBehaviour
{
    [SerializeField] private Transform hand;
    [SerializeField] [Range(0, 2)] private float pushTime = 1f;
    [SerializeField] private Vector3 pushDirection;
    private bool punchable = true;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("sa");
            Push();
        }
    }

    private void Push()
    {
        if (!punchable) return;
        punchable = false;
        this.MakeAction(() => punchable = true, pushTime);
        var seq = DOTween.Sequence();
        seq.Append(hand.DOLocalMove(pushDirection, pushTime / 2));
        seq.Append(hand.DOLocalMove(Vector3.zero , pushTime/2));
        
        
    }
}