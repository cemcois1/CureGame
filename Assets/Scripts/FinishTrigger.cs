using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour, IInteractible
{
     public IMiniGame MiniGame;


     public void Interact()
    {
        GameManager.RunnerFinished?.Invoke();
        this.GetComponent<Collider>().enabled = false;
        GameManager.LoadMinilevel?.Invoke();
        this.MakeAction(() => MiniGame.StartMinigame(UIManager.instance.MoneyCount),2f);

    }
}