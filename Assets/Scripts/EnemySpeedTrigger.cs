using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpeedChanging
{
    INCRESE,
    DECRESE
}

public class EnemySpeedTrigger : MonoBehaviour
{
    [SerializeField] private SpeedChanging speedChangingParameter;

    private void OnTriggerEnter(Collider other)
    {
        switch (speedChangingParameter)
        {
            case SpeedChanging.INCRESE:
                if (other.TryGetComponent(out IRunFaster FastRunable))
                {
                    FastRunable.RunFast();
                }
                break;
            case SpeedChanging.DECRESE:
                if (other.TryGetComponent(out IRunSlowly SlowRunable))
                {
                    SlowRunable.RunSlowly();
                }
                break;
            default:
                break;
        }
    }
}