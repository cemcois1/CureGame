using System;
using TMPro;
using UnityEngine;

namespace Developing.Scripts.HandSystem.GateSystem
{
    [SelectionBase]
    public class PlusGate : MonoBehaviour, IGate
    {
        [SerializeField] private int AddingValue=1;
        [SerializeField] private TextMeshPro text;
        
        private void OnEnable()
        {
            if (!text) return;
            if (AddingValue>0)
            {
                text.text="+ "+AddingValue;
            }
            else
            {
                text.text="- "+(Mathf.Abs( AddingValue));
            }
            
        }
        public virtual void CollideWithObject(Hand hand)
        {
            hand.InstantateDishArray(AddingValue);
        }
        [ContextMenu(nameof(ClosePlaneMesh))]
        void ClosePlaneMesh()
        {
            transform.Find("Plane_1").GetComponent<MeshCollider>().enabled = false;
        }
    }
}