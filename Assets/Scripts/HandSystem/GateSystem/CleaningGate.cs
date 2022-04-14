using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Developing.Scripts.HandSystem.GateSystem
{
    [SelectionBase]
    public class CleaningGate : MonoBehaviour, IGate
    {
        public void CollideWithObject(Hand hand)
        {
            foreach (Dish handHoldingDish in hand.holdingDishes)
            {
                if (!handHoldingDish.isCleaned)
                {
                    handHoldingDish.CleanDish();
                    hand.IncreaseMoney(2,handHoldingDish.transform.position);
                }
            }

            DishPolishEffectForAll(hand, Color.white);
        }

        /*
        private void DishPolishEffectForAll(Hand hand, Color endValue)
        {
            var startCount = hand.holdingDishes.Count;

            for (int i = 0; i < startCount; i++)
            {
                var duration = .2f;
                hand.holdingDishes[i].transform.DOPunchScale(Vector3.one / 5, duration, 0, 1).OnComplete(
                    () => hand.holdingDishes[i].dishMesh.material.DOColor(endValue, duration).From()
                        .SetDelay(i * duration)
                );
            }
        }*/
        private void DishPolishEffectForAll(Hand hand, Color endValue)
        {
            var duration = .2f;

            StartCoroutine(SiraylaYap(hand, duration, endValue));
            /*
            var sequence = DOTween.Sequence();
            var startCount = hand.holdingDishes.Count;

            for (int i = 0; i < startCount; i++)
            {
                sequence.Append(hand.holdingDishes[i].transform.DOPunchScale(Vector3.one / 5, duration, 0, 1));
                var i1 = i;
                this.MakeAction(() =>
                    {
                        if (hand.holdingDishes.Count>=startCount)
                        {
                            hand.holdingDishes[i1].DishPolishEffectForAll(endValue, duration);
                        }
                    },
                    i1 * duration);
            }*/
        }

        private IEnumerator SiraylaYap(Hand hand, float duration, Color endValue)
        {
            var startCount = hand.holdingDishes.Count;
            var waitForSecondsRealtime = new WaitForSecondsRealtime(duration);
            for (int i = 0; i < startCount; i++)
            {
                if (!hand.isPunchScaleable && i+2 >= hand.holdingDishes.Count) break;
                Dish handHoldingDish = hand.holdingDishes[i];
                if (handHoldingDish)
                {
                    handHoldingDish.transform.DOPunchScale(Vector3.one / 5, duration, 0, 1).From();
                    handHoldingDish.DishPolishEffect(endValue, duration);
                }

                if (!handHoldingDish.isCleaned)
                {
                    hand.IncreaseMoney(3,handHoldingDish.transform.position);

                }
                yield return waitForSecondsRealtime;
            }
            

            yield return new WaitForEndOfFrame();
        }
    }
}