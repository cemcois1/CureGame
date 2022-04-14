using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, Time.deltaTime);
    }
}
