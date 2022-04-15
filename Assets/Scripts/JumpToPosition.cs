using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class JumpToPosition : MonoBehaviour
{
    [SerializeField] private Transform[] targetTransforms;
    [SerializeField] private Transform[] objects;
    private static int transformIndex;
    [SerializeField] private float duration;
    [SerializeField] [Range(0, 1)] private float delay;
    [SerializeField] private float jumpPower;
    [SerializeField] private float upValue = 0.1f;

    private void OnMouseDown()
    {
        if (transformIndex >= targetTransforms.Length) return;
        for (int i = 0; i < objects.Length; i++)
        {
            var rangeX = Random.Range(-.02f, .02f);
            var rangeZ = Random.Range(-.02f, .02f);
            objects[i].parent = targetTransforms[transformIndex].GetChild(0);

            var i1 = i;
            objects[i].DOJump(
                    targetTransforms[transformIndex].position +
                    new Vector3((i + 1) * rangeX, upValue, (i + 1) * rangeZ),
                    jumpPower, 1,
                    duration).SetDelay(delay * (i + 1))
                .OnComplete((() => objects[i1].gameObject.AddComponent<Rigidbody>()));
        }

        transformIndex++;
        if (transformIndex == targetTransforms.Length)
        {
            UIManager.instance.OpenDoneButton();
        }
    }
}