using System;
using UnityEngine;

namespace Developing.Scripts
{
    public class FlexableColorChanger : MonoBehaviour
    {
        private FlexibleColorPicker _colorPicker;
        [SerializeField] private Material[] materials;
        [SerializeField] private Material sellectedMaterial;
        

        private void Update()
        {
            sellectedMaterial.color = _colorPicker.color;
        }
    }
}