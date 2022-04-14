using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildHolder : MonoBehaviour
{
    #region Singleton
    public static ChildHolder instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    [SerializeField] GameObject childHolder;
    [SerializeField] WaitForSeconds waitingTime = new WaitForSeconds(3);

    private void LateUpdate()
    {
        childHolder.transform.position = transform.position;
    }
}
