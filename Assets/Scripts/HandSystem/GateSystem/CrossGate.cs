using System;
using TMPro;
using UnityEngine;

namespace Developing.Scripts.HandSystem.GateSystem
{
    [SelectionBase]
    public class CrossGate : MonoBehaviour, IGate
    {
        [SerializeField] private int multiplyValue;
        [SerializeField] private TextMeshPro text;
        private bool playoneTime = true;

        private void OnEnable()
        {
            if (multiplyValue > 0)
            {
                text.text = "X " + multiplyValue;
            }
            else
            {
                text.text = "/ " + -multiplyValue;
            }
        }

        public void CollideWithObject(Hand hand)
        {
            if (!playoneTime) return;
            playoneTime = false;
            if (multiplyValue > 0)
            {
                hand.InstantateDishArray(hand.holdingDishes.Count * (multiplyValue - 1));
            }
            else
            {
                hand.InstantateDishArray(hand.holdingDishes.Count / multiplyValue);
            }
        }

        [ContextMenu("ClosePlaneMesh")]
        void ClosePlaneMesh()
        {
            transform.Find("Plane").GetComponent<MeshCollider>().enabled = false;
        }
    }
}