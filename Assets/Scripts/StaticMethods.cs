using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class StaticMethods
{
    
    public static IEnumerator MakeActionWithDelay(Action action, float delay)
    {
        float tmp = 0;
        while (tmp < delay)
        {
            tmp += Time.deltaTime;
            yield return null;
        }

        action?.Invoke();
        yield return null;
    }

    public static Transform DisableRigidbody(this Transform rigidbodyTransform)
    {
        Rigidbody rigidBody = rigidbodyTransform.GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
        rigidBody.useGravity = false;
        rigidBody.detectCollisions = false;
        return rigidbodyTransform;
    }
    public static Transform EnableRigidbody(this Transform rigidbodyTransform)
    {
        Rigidbody rigidBody = rigidbodyTransform.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.useGravity = true;
        rigidBody.detectCollisions = true;
        return rigidbodyTransform;
    }
    public static Coroutine MakeAction(this MonoBehaviour monoBehaviour, Action action, float delay)
    {
        monoBehaviour.StartCoroutine(MakeActionWithDelay(action, delay));
        return null;
    }

    public static IEnumerator LerpPositionIEnumerator(Vector3 startpos, Vector3 endpos, float duration)
    {
        float t = 0;
        while (t <= 1)
        {
            startpos += Vector3.Lerp(startpos, endpos, t);
            t += Time.deltaTime / duration;
            yield return null;
        }

        startpos = endpos;
        yield return null;
    }
}