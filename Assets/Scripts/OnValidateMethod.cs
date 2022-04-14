using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnValidateMethod : MonoBehaviour
{
    private void OnValidate()
    {
        GetComponent<RectTransform>().transform.localPosition += Vector3.up;
        TextMeshPro textMeshProUGUI = GetComponent<TextMeshPro>();
        textMeshProUGUI.text = (int.Parse(textMeshProUGUI.text) + 5).ToString();
    }
    
}