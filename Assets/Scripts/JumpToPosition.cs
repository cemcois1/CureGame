using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class JumpToPosition : MonoBehaviour
{
    [SerializeField] private Transform[] targetTransforms;
    private static int transformIndex;
    [SerializeField] private float duration;
    [SerializeField] [Range(0, 1)] private float delay;
    [SerializeField] private float jumpPower;
    [SerializeField] private Transform[] objects;


    private void OnMouseDown()
    {
        if (transformIndex >= targetTransforms.Length) return;
        transform.DOJump(targetTransforms[transformIndex].position + Vector3.up * .1f, jumpPower, 1, duration);
        for (int i = 0; i < objects.Length; i++)
        {
            var range = 0;
            objects[i].DOJump(targetTransforms[transformIndex].position + new Vector3(range, (i + 1) * 0.1f, range),
                jumpPower, 1,
                duration).SetDelay(delay * (i + 1));
            ;
        }
        transformIndex++;
        if (transformIndex == targetTransforms.Length)
        {
            UIManager.instance.OpenDoneButton();
        }
    }
}