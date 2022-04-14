using System;
using System.Collections.Generic;
using UnityEngine;

namespace Developing.Scripts.HandSystem
{
    [SelectionBase]
    public class BalanceObjects : MonoBehaviour
    {
        [SerializeField] private Hand _hand;
        [SerializeField] private List<Dish> holdingDishes;
        [SerializeField] private float balanceSpeed = 1;
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] public Transform referanceObject;
        [SerializeField] float length = 8;
        [SerializeField] private float leftBound = -3.5f;
        [SerializeField] private float rightBound = 2;
        [SerializeField] private float startXpos;
        [SerializeField] private bool moveLeft;

        private float timer;

        private void Awake()
        {
            holdingDishes = _hand.holdingDishes;
            startXpos = referanceObject.transform.localPosition.x;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer < .2f) return;
            ChangeReferanceObjectPosition();
            if (holdingDishes.Count == 0) return;
            for (int i = 0; i < holdingDishes.Count; i++)
            {
                if (i == 0)
                {
                    holdingDishes[i].transform.localPosition = Vector3.Lerp(holdingDishes[i].transform.localPosition,
                        new Vector3((referanceObject.localPosition.x - startXpos) / 3 + startXpos,
                            holdingDishes[i].transform.localPosition.y,
                            0), Time.deltaTime * balanceSpeed);
                    continue;
                }

                if (i == 1)
                {
                    holdingDishes[i].transform.localPosition = Vector3.Lerp(holdingDishes[i].transform.localPosition,
                        new Vector3((referanceObject.localPosition.x - startXpos) / 1.5f + startXpos,
                            holdingDishes[i].transform.localPosition.y,
                            0), Time.deltaTime * balanceSpeed);
                    continue;
                }

                if (i == 2)
                {
                    holdingDishes[i].transform.localPosition = Vector3.Lerp(holdingDishes[i].transform.localPosition,
                        new Vector3((referanceObject.localPosition.x - startXpos) / 1.2f + startXpos,
                            holdingDishes[i].transform.localPosition.y,
                            0), Time.deltaTime * balanceSpeed);
                    continue;
                }

                if (i == 3)
                {
                    holdingDishes[i].transform.localPosition = Vector3.Lerp(holdingDishes[i].transform.localPosition,
                        new Vector3(referanceObject.localPosition.x,
                            holdingDishes[i].transform.localPosition.y,
                            0), Time.deltaTime * balanceSpeed);
                    continue;
                }

                {
                    holdingDishes[i].transform.localPosition = Vector3.Lerp(holdingDishes[i].transform.localPosition,
                        new Vector3(holdingDishes[i - 1].transform.localPosition.x,
                            holdingDishes[i].transform.localPosition.y,
                            0), Time.deltaTime * balanceSpeed);
                }
            }
        }

        private void ChangeReferanceObjectPosition()
        {
            if (moveLeft)
            {
                referanceObject.transform.localPosition += (Vector3.left * moveSpeed *
                                                            Time.deltaTime);
            }
            else
            {
                referanceObject.transform.localPosition += (Vector3.right * moveSpeed *
                                                            Time.deltaTime);
            }

            if (startXpos - referanceObject.transform.localPosition.x < leftBound)
            {
                moveLeft = true;
            }

            if (startXpos - referanceObject.transform.localPosition.x > rightBound)
            {
                moveLeft = false;
            }
        }

        [ContextMenu(nameof(ChangeReferanceObjects))]
        private void ChangeReferanceObjects()
        {
            if (gameObject.name == "Hand1")
            {
                referanceObject = transform.Find("LeftPos");
            }

            if (gameObject.name == "Hand2")
            {
                for (int i = 0; i < transform.GetChild(0).childCount; i++)
                {
                    print(transform.GetChild(i).name);
                    if (transform.GetChild(i).name == "Cube")
                    {
                        referanceObject = transform.GetChild(i);
                    }
                }
            }
        }

        [ContextMenu(nameof(ChangePos))]
        private void ChangePos()
        {
            
            if (gameObject.name == "Hand1")
            {
                transform.rotation=Quaternion.Euler(Vector3.right*30);
                transform.localPosition = new Vector3(-4.5f,-0.634f,0f);
            }
            
            if (gameObject.name == "Hand2")
            {
                transform.rotation=Quaternion.Euler(Vector3.right*30);
                transform.localPosition = new Vector3(4.45f,-0.73f,0.1576538f);
            }
        }
    }
}