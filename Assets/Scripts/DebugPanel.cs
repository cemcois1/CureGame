using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    #region Singleton

    public static DebugPanel instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public CustomerSelector[] customerSelectorList;
    [SerializeField] private Material[] materialList;
    [SerializeField] private FlexibleColorPicker _colorPicker;


    public void ChangeCustomerView()
    {
        for (int i = 0; i < customerSelectorList.Length; i++)
        {
            if (customerSelectorList[i].gameObject.activeSelf)
            {
                customerSelectorList[i].changeCustomerfromList();
            }
        }
    }

    public void ChangeMaterial(int index)
    {
        materialList[index].color = _colorPicker.color;
    }
}