using UnityEngine;

namespace Developing.Scripts.HandSystem.GateSystem
{
    
    public class SellGate:MonoBehaviour,IGate
    {
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Vector3 offset;
        [SerializeField] private int sellingCount=4;
        public void CollideWithObject(Hand hand)
        {
            hand.RemoveDish(hand,targetTransform.position+offset,.1f);
            hand.SellDish(sellingCount);
        }
    }
}