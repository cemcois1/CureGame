using System;
using DG.Tweening;
using UnityEngine;

namespace Developing.Scripts.CureGame
{
    public class FreezingPart : Part
    {
        [SerializeField] private Transform[] cupTransforms;
        [SerializeField] private Transform[] cupStartTransforms;
        private Transform[] cupTargettransforms;
        [SerializeField] private Transform[] cupFinishtransforms;
        [SerializeField] private Transform fallingTransform;
        public HorizontalColorManager colorManager;

        public override void StartPart()
        {
            base.StartPart();
            for (int i = 0; i < cupTransforms.Length; i++)
            {
                Destroy(cupTransforms[i].GetComponent<Cup>());
                var cup = cupTransforms[i].gameObject.AddComponent<CupForFilling>();
                cup.finishTransform = cupFinishtransforms[i];
                cup.fallingTransform = fallingTransform;
                cup.popsicleFallTransform = fallingTransform;
                cup.colorManager = colorManager;
                cupTransforms[i].DOMove(cupStartTransforms[i].position, .5f);
            }

            print("FreezingPart");
        }
    }
}