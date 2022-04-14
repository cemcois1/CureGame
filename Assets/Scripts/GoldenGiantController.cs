using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class GoldenGiantController : MonoBehaviour,IHittable,IRunFaster,IRunSlowly
{
    [SerializeField] private Animator animator;
    [SerializeField] private GoldenGiantData config;
    [FormerlySerializedAs("moneyPrefab")] [SerializeField] private GameObject HitFXPrefab;
    [SerializeField] private GameObject BrokenCharacter;
    private int isRunningKeyHesh = Animator.StringToHash("isRuning");
    private int isIdleKeyHesh = Animator.StringToHash("isIdle");
    private IHittable _hittableImplementation;
    private Coroutine coroutine;
    public float CurrentSpeed { get; set; }

    private void OnEnable()
    {
        CurrentSpeed=config.Speed;
        GameManager.LevelStarted += LevelStarted;
        GameManager.RunnerFinished+=StopRunning;
    }
    private void OnDisable()
    {
        GameManager.LevelStarted -= LevelStarted;
        GameManager.RunnerFinished-=StopRunning;

    }
    private void StopRunning()
    {
       StopCoroutine(coroutine);
       TriggerAnimatorToIdle();
    }
    private void LevelStarted()
    {
        
        TriggerAnimatorToRunning();
        TriggerMovementController();
    }

    private void TriggerMovementController()
    {
        transform.position += (Vector3.forward * (CurrentSpeed * Time.deltaTime));
        coroutine= StartCoroutine(MoveForwardCoroutine());
    }

    private IEnumerator MoveForwardCoroutine()
    {
        while (true)
        {
            transform.position += (Vector3.forward * (CurrentSpeed * Time.deltaTime));
            yield return null;
        }
        yield return null;
    }

    private void TriggerAnimatorToRunning()
    {
        animator.SetTrigger(isRunningKeyHesh);
    }
    private void TriggerAnimatorToIdle()
    {
        animator.SetTrigger(isIdleKeyHesh);
    }

    public void Hit()
    {
        //TODO MAKE Falling Animation
        this.MakeAction( ()=>
        {
            Instantiate(BrokenCharacter, transform.position,Quaternion.Euler(0, 180, 0),transform.parent.parent);
            Instantiate(HitFXPrefab, transform.position + Vector3.up * 1f+Vector3.forward*3f, Quaternion.identity,
                    transform.parent.parent);
            Destroy(gameObject);
        },.2f);
    }
    public void RunFast()
    {
        CurrentSpeed = config.SpeedFastVersion;
    }

    public void RunSlowly()
    {
        CurrentSpeed = config.Speed;
    }
}

public interface IRunSlowly
{
    void RunSlowly();
}