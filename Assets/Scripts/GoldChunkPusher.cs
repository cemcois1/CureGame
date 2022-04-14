using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldChunkPusher : MonoBehaviour, IBrokeable
{
    [SerializeField] private Vector3 forceValue;
    [SerializeField]private Rigidbody[] childRigidbodys;
    private void OnEnable()
    {
        Broke();
    }
    public void Broke()
    {
        foreach (Rigidbody rigidbody in childRigidbodys)
        {
            rigidbody.AddForce(forceValue);
        }
        Destroy(this);
    }
}