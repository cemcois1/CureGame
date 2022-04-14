using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHittable data))
        {
            CharacterManager.CharacterStateChanged?.Invoke(CharacterState.Fight);
            data.Hit();
        }

        var tmp = other.GetComponent<IInteractible>();
        if (tmp != null)
        {
            tmp.Interact();
        }

        if (other.TryGetComponent(out IDamageGiveable barrier))
        {
            barrier.HitBarrier();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out IHittable data))
        {
            CharacterManager.CharacterStateChanged?.Invoke(CharacterState.Fight);
            data.Hit();
        }

        var tmp = collision.transform.GetComponent<IInteractible>();
        if (tmp != null)
        {
            tmp.Interact();
        }

        if (collision.transform.TryGetComponent(out IDamageGiveable barrier))
        {
            barrier.HitBarrier();
        }
    }
}