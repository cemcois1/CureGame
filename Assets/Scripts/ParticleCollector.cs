using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ParticleCollector : MonoBehaviour
{
    [FormerlySerializedAs("CollectionTransform")] [SerializeField]
    private List<Transform> CollectionTransforms = new List<Transform>();

    [FormerlySerializedAs("CollectionTransform")] [SerializeField]
    private List<Transform> ThrowingTransforms = new List<Transform>();


    [FormerlySerializedAs("pointcCounter")] [FormerlySerializedAs("counter")] [SerializeField]
    private int pointCounter = 0;

    [SerializeField] private int passinglimit = 60;
    [SerializeField] private int loseableChunkCount = 5;

    private int index = 0;

    [FormerlySerializedAs("startValue")] [Header("CinemachineValues")] [SerializeField]
    private float offsetCameraToPlayer;


    private void OnEnable()
    {
        CharacterManager.CharacterStateChanged += LoseGold;
    }

    private void OnDisable()
    {
        CharacterManager.CharacterStateChanged -= LoseGold;
    }

    private void LoseGold(CharacterState obj)
    {
        if (obj != CharacterState.Fall) return;
        var time = .2f;
        if (CollectionTransforms[index].transform.childCount <= loseableChunkCount)
        {
            for (int i = 0; i < CollectionTransforms[index].transform.childCount; i++)
            {
                ThrowParticle(i, time);
            }

            if (index > 0)
            {
                RemoveVagon();
            }
            else
            {
                CheckGameOver();
            }
        }

        if (CollectionTransforms[index].transform.childCount > loseableChunkCount)
        {
            for (int i = 0; i < loseableChunkCount; i++)
            {
                ThrowParticle(i, time);
            }
        }
        else
        {
            if (CollectionTransforms[index].transform.childCount > loseableChunkCount)
            {
                for (int i = 0; i < loseableChunkCount; i++)
                {
                    ThrowParticle(i, time);
                }
            }
            else
            {
                for (int i = 0; i < CollectionTransforms[index].transform.childCount; i++)
                {
                    ThrowParticle(i, time);
                }
            }
        }
    }

    private void CheckGameOver()
    {
        if (index == 0 && CollectionTransforms[0].childCount <= 0)
        {
            GameManager.LevelFailed?.Invoke();
        }
    }

    private void ThrowParticle(int i, float time)
    {
        if (CollectionTransforms[0].transform.childCount == 0 && index == 0) return;
        Transform child;
        child = CollectionTransforms[index].transform.GetChild(i);

        pointCounter -= child.GetComponent<ICollectible>().Point;
        child.transform
            .DOJump(ThrowingTransforms[index].position + Vector3.left * Random.Range(-3f, 3f), 2f, 1, time)
            .OnComplete(() => Destroy(child.gameObject));
    }

    private void RemoveVagon()
    {
        CollectionTransforms[index].parent.gameObject.SetActive((false));
        index--;
        CinemachineManager.ChangeFollowOffset?.Invoke(-offsetCameraToPlayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollectible>(out ICollectible item))
        {
            if (pointCounter >= passinglimit * (index + 1) && index < CollectionTransforms.Count - 1)
            {
                AddVagon();
            }

            StartCoroutine(SetTransform(other));
            this.MakeAction(() =>
            {
                pointCounter += item.Point;
                item.Collect();
                AudioManager.ItemCollectedSound?.Invoke();
            }, .5f);
            //Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            //rigidbody.isKinematic = true;
            //rigidbody.useGravity = false;
            /*other.transform.DOJump(CollectionTransform.position, jumpPower, 1, .1f).OnComplete(() =>
            {
                other.transform.localPosition=Vector3.zero;
            });*/
        }
    }

    private IEnumerator SetTransform(Collider other)
    {
        var duration = .5f;
        var component = other.GetComponent<Rigidbody>();
        component.detectCollisions = false;
        other.transform.SetParent(CollectionTransforms[index], true);
        //other.transform.localPosition = Vector3.zero;
        other.transform.localScale = other.transform.localScale * 1.5f;
        other.transform.DOLocalJump(CollectionTransforms[index].localPosition, 1f, 1, duration)
            .OnComplete(() => component.detectCollisions = true);
        yield return new WaitForSeconds(duration);
        //other.transform.position = CollectionTransforms[index].position;
        //other.transform.localPosition = Vector3.zero;
        try
        {
            component.velocity = Vector3.zero;
            component.constraints = RigidbodyConstraints.None;
        }
        catch (Exception e)
        {
            print("Bug Fixed");
        }
    }

    private void AddVagon()
    {
        index++;
        CollectionTransforms[index].parent.gameObject.SetActive((true));
        CinemachineManager.ChangeFollowOffset?.Invoke(offsetCameraToPlayer);
    }

    [Header("For Setting collection and adding Transforms")] [SerializeField]
    private Transform parentTransform;


    [ContextMenu("SortPointsIncreasedly")]
    public void SetTransforms()
    {
        print(parentTransform.childCount);
        CollectionTransforms.Clear();
        ThrowingTransforms.Clear();
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            CollectionTransforms.Add(parentTransform.GetChild(i).Find("VagonAddingPosition"));
            if (parentTransform.GetChild(i).Find("ThrowingPosition") != null)
            {
                ThrowingTransforms.Add(parentTransform.GetChild(i).Find("ThrowingPosition"));
            }
            else
            {
                ThrowingTransforms.Add(parentTransform.GetChild(i).Find("ThrowingPosition_1"));
            }
        }
    }
}