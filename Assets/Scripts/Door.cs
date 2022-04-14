using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum DoorColor
{
    Green,
    Red
}
[SelectionBase]
public class Door : MonoBehaviour, IInteractible
{

    [SerializeField] private DoorColor color;
    [SerializeField] private int Count;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string textInput;
    public void Interact()
    {
        switch (color)
        {
            case DoorColor.Green:
                AddEmployee(Count);
                break;
            case DoorColor.Red:
                LostMoney(Count);
                break;
            default:
                break;
        }
    }
    private void OnEnable()
    {
        UpdateDoorText();
    }

    private void UpdateDoorText()
    {
        if (text == null)
        {
        }
        else
            text.text = textInput;
        //switch (color)
        //{
        //    case DoorColor.Red:
        //        text.text = "- " + Count.ToString();
        //        break;
        //    case DoorColor.Green:

        //        text.text = "+ " + Count.ToString();
        //        break;


        //    default:
        //        break;
        //}
    }
    private void LostMoney(int Count)
    {
        CharacterManager.AddEmployees?.Invoke(-Count);
    }
    private void AddEmployee(int addingCount)
    {
        CharacterManager.AddEmployees?.Invoke(addingCount);
    }
}
