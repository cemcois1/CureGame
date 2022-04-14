using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    void LateUpdate()
    {
        transform.Rotate(Vector3.up, Space.World);
    }
}
