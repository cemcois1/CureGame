using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftTexture : MonoBehaviour
{
    [SerializeField] private float speed;

    private MeshRenderer
    meshRenderer;

    private void Awake()
    {
        meshRenderer= GetComponent<MeshRenderer>();
    }

    void Update()
    {
        
        meshRenderer.sharedMaterial.mainTextureOffset += Vector2.up * Time.deltaTime * speed;
    }
}