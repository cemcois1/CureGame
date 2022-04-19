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
        [SerializeField] private CustomerSelector customerSelector;
        
        /// <summary>
        /// must be parent of pupsicle
        /// </summary>
        [SerializeField] private Transform pupsicleCup;

        [SerializeField] private Transform pupsicle;

        [SerializeField] private Transform fallingTransform;
        public HorizontalColorManager colorManager;
        [SerializeField] private Transform frigeTransform;
        private Vector3 popsicleStartPosition;
        [SerializeField] private Transform customerHandTransform;
        [SerializeField] private Transform frigeKapakTransform;

        private void OnEnable()
        {
            popsicleStartPosition = pupsicleCup.position;
        }

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
                cup.pupsicleCup = pupsicleCup;
                cup.pupsicle = pupsicle;
                cup.frigeTransform = frigeTransform;
                cup.popsicleStartPosition = popsicleStartPosition;
                cup.frigeKapakTransform = frigeKapakTransform;
                cup.customerHandTransform = customerHandTransform;
                cup.customSellector = customerSelector;
            }

        }
    }
}