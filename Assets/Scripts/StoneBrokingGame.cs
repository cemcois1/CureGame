using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class StoneBrokingGame : MonoBehaviour, IMiniGame
{
    [Header("Vagon Values")] [FormerlySerializedAs("VagonStartTransform")] [SerializeField]
    private Transform[] vagonStartTransform;

    [SerializeField] private Transform[] Vagons;
    [SerializeField] private Transform[] VagonAddingPositions;

    [Header("Player Values")] [SerializeField]
    private GameObject Player;

    [SerializeField] private ParticleCollector goldenCollector;
    [SerializeField] private GameObject Kazma;
    [SerializeField] private GameObject PlayerFinishPos;
    [Header("Others")] [SerializeField] private GameObject Heykel;
    [SerializeField] private Transform[] Particletransforms;
    [SerializeField] private FinishTrigger finishTrigger;
    [SerializeField] private ParticleSystem[] ParticleEffect;
    [SerializeField] private Transform UsedParticleParent;
    private int VagonCount;
    private int nullcount;


    private void OnEnable()
    {
        //finishTrigger.MiniGame = this;
    }


    public void StartMinigame()
    {
        StartCoroutine(StartMinigameIEnumerator());
    }

    public void StartMinigame(int score)
    {
        
    }

    private IEnumerator StartMinigameIEnumerator()
    {
        goldenCollector.gameObject.SetActive(false);
        Kazma.SetActive(false);
        Player.transform.DOMove(PlayerFinishPos.transform.position, .5f);
        Player.transform.DORotate(PlayerFinishPos.transform.rotation.eulerAngles, .5f);
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < Vagons.Length; i++)
        {
            Vagons[i].transform.DOMove(vagonStartTransform[i].position, 1f);
            
            
            yield return new WaitForSeconds(.2f);
        }


        StartCoroutine(FillScapulpture(.05f, .05f));
    }

    private IEnumerator FillScapulpture(float delay, float flowDuration)
    {
        VagonCount = VagonAddingPositions.Length - 1;
        for (int i = 0; i < Particletransforms.Length; i++)
        {
            var value = GetParticleFromVagons();
            if (value == null && VagonCount > 0)
            {
                value = GetParticleFromVagons();
                value = value ? GetParticleFromVagons() : value;
                
            }

            value?.DOMove(Particletransforms[i].position, flowDuration)
                .OnComplete(()=>AudioManager.LevelEndFillingSound?.Invoke());
            if (nullcount>10)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(delay);

            }
        }

        GameEnded();

        yield return null;
    }

    private void GameEnded()
    {
        if (GetParticleFromVagons() == null)
        {
            if (GetParticleFromVagons() == null)
            {
                print("Normal Win");
                FallParticles(UsedParticleParent);
                GameManager.MinilevelFinished?.Invoke();
                AudioManager.FinalAudioClipSound?.Invoke();
                return;
            }

            print("BugFixed");
        }

        print("Perfect Win");
        AudioManager.FinalAudioClipSound?.Invoke();
        UsedParticleParent.gameObject.SetActive(false);
        ShowScoulpt();
        PlayParticle();
        GameManager.MinilevelFinished?.Invoke();
    }

    private void FallParticles(Transform usedParticleParent)
    {
        for (int i = 0; i < usedParticleParent.childCount; i++)
        {
            usedParticleParent.GetChild(i).EnableRigidbody();
        }
    }

    private void PlayParticle()
    {
        ParticleEffect[0].Play();
        ParticleEffect[1].Play();
    }

    private void ShowScoulpt()
    {
        Heykel.GetComponent<MeshRenderer>().enabled = true;
        Heykel.GetComponent<MeshRenderer>().material.DOColor(Color.white, .5f).From();
    }

    private Transform GetParticleFromVagons()
    {
        if (VagonAddingPositions[VagonCount].childCount > 0)
        {
            var child = VagonAddingPositions[VagonCount].GetChild(0).DisableRigidbody();
            child.parent = UsedParticleParent;
            return child;
        }
        else
        {
            if (VagonCount > 0) VagonCount--;
            if (VagonAddingPositions[VagonCount].childCount > 0)
            {
                var child = VagonAddingPositions[VagonCount].GetChild(0).DisableRigidbody();


                child.parent = UsedParticleParent;
                return child;
            }
            else
            {
                print("null");
                nullcount++;
                if (nullcount>10)
                {
                    
                }
                return null;
            }
        }

        print("Null");
        return null;
    }

    #region ContextMenuMethods

    [ContextMenu("SortPointsDecreasedly")]
    private void SortPointsDecreasedly()
    {
        for (int i = 0; i < Particletransforms.Length; i++)
        {
            for (int j = 0; j < Particletransforms.Length - 1; j++)
            {
                if (Particletransforms[i].position.y > Particletransforms[j].position.y)
                {
                    Transform bosKova;
                    bosKova = Particletransforms[i];
                    Particletransforms[i] = Particletransforms[j];
                    Particletransforms[j] = bosKova;
                }
            }
        }
    }

    [ContextMenu("SortPointsIncreasedly")]
    private void SortPointsIncreasedly()
    {
        for (int i = 0; i < Particletransforms.Length; i++)
        {
            for (int j = 0; j < Particletransforms.Length - 1; j++)
            {
                if (Particletransforms[i].position.y < Particletransforms[j].position.y)
                {
                    Transform bosKova;
                    bosKova = Particletransforms[j];
                    Particletransforms[j] = Particletransforms[i];
                    Particletransforms[i] = bosKova;
                }
            }
        }
    }

    #endregion
}