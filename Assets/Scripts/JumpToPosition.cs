using System;
using System.Collections;
using Developing.Scripts.CureGame;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[SelectionBase]
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
    [SerializeField] private int randomDivideValue = 2;
    private bool isClickable = true;

    private void OnEnable()
    {
        transformIndex = 0;
    }

    private void OnMouseDown()
    {
        
        if (!isClickable) return;
        if (transformIndex >= cupTransforms.Length) return;
        AudioManager.instance.ItemSellectedSound();
        for (int i = 0; i < objects.Length; i++)
        {
            float rangeX;
            float rangeZ;
            if (gameObject.name.Contains("BlueBerries") || gameObject.name.Contains("Blackberrys"))
            {
                rangeX = 0;
                rangeZ = 0;
            }
            else
            {
                rangeX = Random.Range(-.01f, .01f);
                rangeZ = Random.Range(-.01f, .01f);
            }

            objects[i].parent = cupTransforms[transformIndex].GetChild(0);
            var i1 = i;

            objects[i].DOJump(
                    cupTransforms[transformIndex].position +
                    new Vector3((i / randomDivideValue + 1) * rangeX, upValue, (i / randomDivideValue + 1) * rangeZ),
                    jumpPower, 1,
                    duration).SetDelay(delay * (i + 1))
                .OnComplete((() =>
                {
                    objects[i1].gameObject.AddComponent<Rigidbody>();
                    objects[i1].ResetRigidbodyMovement();
                }));
        }

        cupTransforms[transformIndex].gameObject.GetComponent<ColourHolder>().color = fruitColor;
        transformIndex++;
        isClickable = false;
        if (transformIndex == cupTransforms.Length)
        {
            UIManager.instance.OpenDoneButton();
        }
    }
}