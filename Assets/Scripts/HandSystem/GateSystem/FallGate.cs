using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Developing.Scripts.HandSystem.GateSystem
{
    [SelectionBase]
    public class FallGate : PlusGate
    {
        [SerializeField] private int holdingValue = 10;

        /// 15 tabak var
        ///
        /// istemek 5 tane kalması icin
        /// -(15- 10) gondermem gerek
        public override void CollideWithObject(Hand hand)
        {
            if (hand.holdingDishes.Count < holdingValue) return;
            var minesValue = -Mathf.Abs(hand.holdingDishes.Count - holdingValue);
            hand.InstantateDishArray(minesValue);
        }
    }
}