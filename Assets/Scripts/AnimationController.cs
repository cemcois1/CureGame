using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private Animator animator;
    [SerializeField] private float animationSpeed = 1;
    private readonly int isRuningHash = Animator.StringToHash("isRuning");
    private readonly int isWiningHash = Animator.StringToHash("isWining");
    private readonly int isIdleHash = Animator.StringToHash("isIdle");
    private readonly int isFailedHash = Animator.StringToHash("isFailed");
    private readonly int isHitedHash = Animator.StringToHash("isHited");
    private readonly int isFalledHash=  Animator.StringToHash("isFalled");
    
    private int AnimationspeedHash = Animator.StringToHash("Speed");
    void Reset()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        GameManager.LevelStarted += OnLevelStarted;
        GameManager.LevelFailed += OnLevelFailed;
        CharacterManager.CharacterStateChanged += ChangeCharacterState;
    }
    private void OnDisable()
    {
        GameManager.LevelStarted -= OnLevelStarted;
        GameManager.LevelFailed -= OnLevelFailed;
        CharacterManager.CharacterStateChanged -= ChangeCharacterState;
    }
    private void OnLevelStarted()
    {
        CharacterManager.CharacterStateChanged(CharacterState.Running);
    }
    private void OnLevelFailed()
    {
        print("Level Failed");
        CharacterManager.CharacterStateChanged(CharacterState.Dead);
    }

    private void ChangeCharacterState(CharacterState State)
    {
        ChangeState(State, animator);

    }

    private void ChangeChildsState(CharacterState State)
    {
        foreach (GameObject item in characterManager.Employees) //Change Childs State
        {
            if (State != CharacterState.Win && item.activeInHierarchy)
            {
                ChangeState(State, item.GetComponent<Animator>());
            }
        }
    }

    private void ChangeState(CharacterState State, Animator animator)
    {
        switch (State)
        {
            case CharacterState.None:
                break;
            case CharacterState.Idle:
                animator.SetTrigger(isIdleHash);
                print("Idle Anim");
                break;
            case CharacterState.Running:
                print("Running Anim");
                animator.SetTrigger(isRuningHash);
                animator.SetFloat(AnimationspeedHash, animationSpeed);
                break;
            case CharacterState.Dead:
                print("Dead Anim");
                animator.SetTrigger(isFailedHash);
                break;
            case CharacterState.Win:
                animator.SetTrigger(isWiningHash);
                //animator.SetTrigger(isIdleHash);
                //StartCoroutine(StaticMethods.MakeActionWithDelay(() => animator.SetTrigger(isWiningHash), 1));
                break;
            case CharacterState.Fight:
                animator.SetTrigger(isHitedHash);
                break;
            case CharacterState.Fall:
                print("Fall Anim");
                animator.SetTrigger(isFalledHash);
                break;
            default:
                break;
        }
    }

    #region Editor Methods
    /// <summary>
    /// For testing
    /// </summary>
    public void SetSpeed()
    {
        animator.SetTrigger(isRuningHash);
        animator.SetFloat(AnimationspeedHash, animationSpeed);
        print("Speed Seted!");
    }
    #endregion
}
