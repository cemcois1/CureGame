using System;
using System.Collections;
using Developing.Scripts.CureGame;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class JumpToPosition : MonoBehaviour
{
    [FormerlySerializedAs("targetTransforms")] [SerializeField]
    private Transform[] cupTransforms;

    [SerializeField] private Transform[] objects;
    private static int transformIndex;
    [SerializeField] private float duration;
    [SerializeField] [Range(0, 1)] private float delay;
    [SerializeField] private float jumpPower;
    [SerializeField] private float upValue = 0.1f;
    [SerializeField] private Color fruitColor;

    private void OnMouseDown()
    {
        if (transformIndex >= cupTransforms.Length) return;
        for (int i = 0; i < objects.Length; i++)
        {
            var rangeX = Random.Range(-.02f, .02f);
            var rangeZ = Random.Range(-.02f, .02f);
            objects[i].parent = cupTransforms[transformIndex].GetChild(0);
            var i1 = i;
            objects[i].DOJump(
                    cupTransforms[transformIndex].position +
                    new Vector3((i + 1) * rangeX, upValue, (i + 1) * rangeZ),
                    jumpPower, 1,
                    duration).SetDelay(delay * (i + 1))
                .OnComplete((() => { objects[i1].gameObject.AddComponent<Rigidbody>(); }));
        }

        cupTransforms[transformIndex].gameObject.GetComponent<ColourHolder>().color = fruitColor;
        transformIndex++;
        if (transformIndex == cupTransforms.Length)
        {
            UIManager.instance.OpenDoneButton();
        }
    }
}