using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
[DefaultExecutionOrder(400)]
public class BindToCamera : MonoBehaviour
{
    [SerializeField][Range(0f,1f)] private float weight=.5f;

}
