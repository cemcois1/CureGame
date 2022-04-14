using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 MovingVector = Vector3.up;
    public float speed = 1;
    [SerializeField] private List<Transform> gameObjects;
    private bool isMoveable = false;

    void Update()
    {
        if (isMoveable)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].position += MovingVector.normalized * (Time.deltaTime * speed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Dish dish))
        {
            gameObjects.Add(dish.transform);
            isMoveable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Dish dish))
        {
            gameObjects.Remove(dish.transform);
            isMoveable = true;
        }
    }
}
