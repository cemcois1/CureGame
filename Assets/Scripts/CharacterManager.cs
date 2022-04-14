using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class CharacterManager : MonoBehaviour
{
    #region Game Start
    private bool isGameStarted = false;
    #endregion

    public GameObject[] Employees;
    public static Action<CharacterState> CharacterStateChanged;
    public static int EmployeeCounterForDebuging = 0;
    [SerializeField] private MovementController movementController;
    /// <summary>
    /// if value is negative remove employees
    /// </summary>
    public static Action<int> AddEmployees;
    private void OnEnable()
    {
        AddEmployees += AddEmployee;
        UIManager.instance.characterTransform = transform;
    }


    private void OnDisable()
    {
        AddEmployees -= AddEmployee;
    }

    private void Start()
    {
        EmployeeCounterForDebuging = 0;
    }
    private void Reset()
    {
        movementController = GetComponent<MovementController>();
    }
    private void AddEmployee(int addingCount)
    {
        if (addingCount == 0) return;
        if (addingCount > -0.1f)
        {
            foreach (GameObject item in Employees)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.GetComponent<Animator>().WriteDefaultValues();
                    item.GetComponent<Animator>().SetTrigger("isRuning");
                    addingCount--;
                    EmployeeCounterForDebuging++;
                    if (addingCount == 0) break;
                }
            }
        }
        else if (addingCount < 0)
        {
            foreach (GameObject item in Employees)
            {
                if (item.activeSelf)
                {
                    item.SetActive(false);
                    addingCount++;
                    EmployeeCounterForDebuging--;
                    if (addingCount == 0) break;
                }
            }
        }
        SetBounces();

    }

    private void SetBounces()
    {
        if (EmployeeCounterForDebuging > 1)
        {
            movementController.BounceLeft = -1.8f;
        }
        if (EmployeeCounterForDebuging > 0)//set Bounces
        {
            movementController.BounceRight = 1.8f;

        }
        else//set default
        {
            movementController.BounceRight = 2.8f;
            movementController.BounceLeft = -2.8f;
        }
    }

    private void MoveLeft()
    {
        foreach (GameObject item in Employees)
        {
            if (item.activeInHierarchy)
            {
                item.transform.DORotate(Vector3.up * -90, 1);
                item.transform.DOMove(transform.position + Vector3.forward * 5 + Vector3.left * 10, 1);
            }
        }
        GetComponent<Rigidbody>().AddForce(Vector3.up * 80f);
    }
}
