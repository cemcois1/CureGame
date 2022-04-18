using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    public static DebugPanel instance;
    [HideInInspector] public CustomerSelector customerSelector;
    [SerializeField] private Material[] materialList;
    [SerializeField] private FlexibleColorPicker _colorPicker;

    private void Awake()
    {
        instance = this;
    }
    public void ChangeCustomerView()
    {
        customerSelector.changeCustomerfromList();
    }

    public void ChangeMaterial(int index)
    {
        materialList[index].color = _colorPicker.color;
    }
}