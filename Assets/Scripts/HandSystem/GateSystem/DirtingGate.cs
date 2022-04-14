using DG.Tweening;
using UnityEngine;
using System.Collections;

namespace Developing.Scripts.HandSystem.GateSystem
{
    [SelectionBase]
    public class DirtingGate : MonoBehaviour, IGate
    {
        public void CollideWithObject(Hand hand)
        {
            foreach (Dish handHoldingDish in hand.holdingDishes)
            {
                handHoldingDish.DirtyDish();
            }

            DishPolishEffect(hand, Color.yellow);
        }

        private void DishPolishEffect(Hand hand, Color endValue)
        {
            var duration = .1f;

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
                            hand.holdingDishes[i1].DishPolishEffect(endValue, duration);
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
                if (!hand.isPunchScaleable && i+3 >= hand.holdingDishes.Count) break;
                Dish handHoldingDish = hand.holdingDishes[i];
                if (handHoldingDish)
                {
                    handHoldingDish.transform.DOPunchScale(Vector3.one / 5, duration, 0, 1);
                    handHoldingDish.DishPolishEffect(endValue, duration);
                }


                yield return waitForSecondsRealtime;
            }

            yield return null;
        }
        /*
    private void DishPolishEffect(Hand hand, Color endValue)
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
    }
    */
    }
}