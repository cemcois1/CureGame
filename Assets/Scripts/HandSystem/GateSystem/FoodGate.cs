using System.Collections;
using DG.Tweening;
using UnityEngine;


namespace Developing.Scripts.HandSystem.GateSystem
{
    [SelectionBase]
    public class FoodGate : MonoBehaviour, IGate
    {
        public void CollideWithObject(Hand hand)
        {
            foreach (Dish handHoldingDish in hand.holdingDishes)
            {
                if (handHoldingDish.sweetType == SweetType.empty)
                {
                    handHoldingDish.sweetType = SweetType.donut1;
                    hand.IncreaseMoney(3,handHoldingDish.transform.position);
                }
                else if (handHoldingDish.sweetType == SweetType.wafle)
                {

                }
                else
                {
                    handHoldingDish.sweetType = handHoldingDish.sweetType + 1;
                    hand.IncreaseMoney(3,handHoldingDish.transform.position);

                }

                handHoldingDish.AddSweet(handHoldingDish.sweetType);
            }

            DishPolishEffect(hand, Color.white);
        }

        private void DishPolishEffect(Hand hand, Color endValue)
        {
            var duration = .2f;

            StartCoroutine(AyniAndaYap(hand, duration, endValue));
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

        private IEnumerator AyniAndaYap(Hand hand, float duration, Color endValue)
        {
            var startCount = hand.holdingDishes.Count;
            var waitForSecondsRealtime = new WaitForSecondsRealtime(duration);
            for (int i = 0; i < startCount; i++)
            {
                if (!hand.isPunchScaleable && i + 2 >= hand.holdingDishes.Count) break;
                Dish handHoldingDish = hand.holdingDishes[i];
                if (handHoldingDish)
                {
                    handHoldingDish.transform.DOPunchScale(Vector3.one / 5, duration, 0, 1);
                    handHoldingDish.DishPolishEffect(endValue, duration);
                }
            }

            yield return null;
        }
    }
}