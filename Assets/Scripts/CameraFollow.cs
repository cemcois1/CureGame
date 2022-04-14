using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    private Coroutine cameraFollow;
    private Vector3 offset;
    [SerializeField] private float smoothSpeed = 5;
    [Space]
    [Header("For Testing")]
    [SerializeField] private GameObject playerPosition;
    [SerializeField] private Transform levelEndPosition;
    [SerializeField] private Transform levelFailPosition;
    private void Reset()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        offset = playerPosition.transform.position - transform.position;
        cameraFollow = StartCoroutine(FollowPlayer());
    }
    private void OnEnable()
    {
        #region Events
        //GameManager.RunnerFinished += GoCameraEndTransform;
        GameManager.LoadLevelEndCamera += DontFollowPlayer;
        GameManager.LevelFailed += DontFollowPlayer;
        GameManager.LevelFailed += GoLevelFailedPosition;
        #endregion
    }



    private void OnDisable()
    {
        #region Events Disable
       // GameManager.RunnerFinished -= GoCameraEndTransform;
        GameManager.LoadLevelEndCamera -= DontFollowPlayer;
        GameManager.LevelFailed -= DontFollowPlayer;
        GameManager.LevelFailed -= GoLevelFailedPosition;
        #endregion
    }
    private IEnumerator FollowPlayer()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, playerPosition.transform.position - offset, Time.deltaTime * smoothSpeed);
            yield return null;
        }
    }
    private IEnumerator RotateCamera(Transform target)
    {
        while (true)
        {
            transform.LookAt(target, Vector3.up);
            yield return null;
        }
    }
    private void GoCameraEndTransform()
    {
        transform.DOLocalMove(levelEndPosition.position, 1);
        transform.DOLocalRotateQuaternion(levelEndPosition.rotation, 1);
    }
    private void DontFollowPlayer()
    {
        StopCoroutine(cameraFollow);
    }

    private void GoLevelFailedPosition()
    {
        transform.DOMove(levelFailPosition.position, 1);
        transform.DORotateQuaternion(levelFailPosition.rotation, 1);
    }
}

