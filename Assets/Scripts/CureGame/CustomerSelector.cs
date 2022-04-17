using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] customerlist;
    [SerializeField] private int index;

    [ContextMenu("changeCustomerList")]
    public void changeCustomerList()
    {
        for (int i = 0; i < customerlist.Length; i++)
        {
            customerlist[i].SetActive(false);
        }

        print("Worked");
        customerlist[index%customerlist.Length].SetActive(true);
        index++;
    }
}