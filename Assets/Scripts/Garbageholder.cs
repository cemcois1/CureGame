using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbageholder : MonoBehaviour
{
    public static Garbageholder instance;

    private void Awake()
    {
        instance = this;
    }
}