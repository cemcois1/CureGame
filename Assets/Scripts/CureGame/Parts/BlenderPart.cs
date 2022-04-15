using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Developing.Scripts.CureGame
{
    public class BlenderPart : Part
    {
        [SerializeField] private Transform[] cups;
        [SerializeField] private Animator blender;

        [FormerlySerializedAs("cuptransforms")] [SerializeField]
        private Transform[] cupTargettransforms;

        [SerializeField] private Transform[] cupFinishtransforms;

        [SerializeField] private float throwTime = .5f;
        [SerializeField] private Transform fallingTransform;
        [SerializeField] private Transform fillingTransform;


        public override void StartPart()
        {
            base.StartPart();
            StartCoroutine(StartPartIEnumerator());
        }

        IEnumerator StartPartIEnumerator()
        {
            print("Blender Part Loading..");
            yield return new WaitForSeconds(1.5f);
            blender.enabled = true;
            for (int i = 0; i < cups.Length; i++)
            {
                cups[i].transform.DOMove(cupTargettransforms[i].position, throwTime);
                yield return new WaitForSeconds(throwTime);
                var component = cups[i].gameObject.AddComponent<Cup>();
                component.duration = throwTime;
                component.targetTransform = fallingTransform;
                component.startPosition = cups[i].transform.position;
                component.fillingTransform = fillingTransform;
                component.finalTransform = cupFinishtransforms[i];
            }
        }
    }
}