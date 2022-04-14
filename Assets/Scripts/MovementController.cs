using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour
{
    [Space]
    [Header("Speed Variables")]
    [SerializeField] private float slideSpeed = 1;
    [SerializeField] private float forwardSpeed = 1;

    [Space]
    [Header("Bounce Limits")]
    public float BounceRight = 0;
    public float BounceLeft = 0;


    private Coroutine Runningcoroutine;
    [SerializeField] private InputControlller inputControlller;
    private void Reset()
    {
        inputControlller = GetComponent<InputControlller>();
    }
    private void OnEnable()
    {
        inputControlller.OnMousePositionChanged += MoveXAxis;
        GameManager.LevelStarted += StartRunning;
        GameManager.RunnerFinished += StopRunning;
        GameManager.LevelFailed += StopRunning;
    }
    private void OnDisable()
    {
        inputControlller.OnMousePositionChanged -= MoveXAxis;
        GameManager.LevelStarted -= StartRunning;
        GameManager.RunnerFinished -= StopRunning;
        GameManager.LevelFailed -= StopRunning;
    }
    public void StartRunning()
    {
        Runningcoroutine = StartCoroutine(MoveCoroutine());

    }
    private void StopRunning()
    {
        if (Runningcoroutine != null)
        {
            StopCoroutine(Runningcoroutine);
        }
        CharacterManager.CharacterStateChanged(CharacterState.Win);
        
    }
    public IEnumerator MoveCoroutine()
    {
        while (true)
        {
            transform.position += Vector3.forward * Time.deltaTime * forwardSpeed;
            yield return null;
        }
    }
    private void MoveXAxis()
    {
        switch (inputControlller.inputType)
        {
            case InputType.TapAndDrag:
                if (transform.position.x <= BounceRight && transform.position.x >= BounceLeft)
                {
                    transform.position += inputControlller.DistanceX * Time.deltaTime * Vector3.left;
                }
                break;
            case InputType.tapAndDragV2:
                transform.position += transform.right * Time.deltaTime * slideSpeed * inputControlller.DistanceX;
                break;
            default:
                break;
        }
        LimitPosition();
    }

    private void LimitPosition()
    {
        if (transform.position.x >= BounceRight)
            transform.position = new Vector3(BounceRight, transform.position.y, transform.position.z);
        if (transform.position.x <= BounceLeft)
            transform.position = new Vector3(BounceLeft, transform.position.y, transform.position.z);
    }


}
