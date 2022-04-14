using System;
using UnityEngine;

namespace Developing.Scripts.HorizontalLimitlessCreatingSystem
{
    public class HorizontalEndlessItemCreator : MonoBehaviour
    {
        [SerializeField] private float startYPosition;
        [SerializeField] private float horizontalOffset;
        [SerializeField] private int itemCount;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform itemParent;


        private void OnEnable()
        {
            startYPosition = transform.position.y;
        }

        private void Update()
        {
            if (transform.position.y > startYPosition + horizontalOffset * (itemCount))
            {
                Instantiate(prefab,
                    new Vector3(transform.position.x, (itemCount-1 + startYPosition), transform.position.z),
                    Quaternion.Euler(0,-90,0), itemParent);
                itemCount++;
            }
        }
    }
}