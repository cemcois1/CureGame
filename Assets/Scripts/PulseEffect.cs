using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PulseEffect : MonoBehaviour
{
    [SerializeField] public float speed = 50f;
    [SerializeField] public float currentspeed = 50f;

    [FormerlySerializedAs("pulseValue")] [SerializeField]
    private float pulseStartValue = 10f;

    private float pulseCurrentValue = 10f;
    private float startRotation;

    private void Start()
    {
            startRotation = Random.Range(0f, 15f);
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(Mathf.PingPong(Time.time * currentspeed, pulseCurrentValue), 0
            , Mathf.PingPong(Time.time * currentspeed, pulseCurrentValue + startRotation)));
    }
}