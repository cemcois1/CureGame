using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PingPong : MonoBehaviour
{
    [SerializeField] private float speed=1f;
    [SerializeField] private float lenght=.5f;


    void Update()
    {
        transform.position += Vector3.right * (Mathf.PingPong(Time.time * speed, lenght)-lenght/2);
    }
}