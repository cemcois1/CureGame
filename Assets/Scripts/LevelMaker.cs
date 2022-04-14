using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelMaker : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private Vector3[] offsets;

    [SerializeField] private float barieroffset;
    [SerializeField] private float startoffset;

    [SerializeField] private int[] prefabIndexes;

    [SerializeField] private Vector3 rotationVector = Vector3.zero;
    private bool playOneTime = true;

    private void OnEnable()
    {
        //PrepareLevel();
    }

    private void OnDisable()
    {
        //DeleteLevel();
    }

    [ContextMenu(nameof(DeleteLevel))]
    private void DeleteLevel()
    {
        var transformChildCount = transform.childCount;
        for (int i = 0; i < transformChildCount; i++)
        {
#if UNITY_EDITOR
            DestroyImmediate(transform.GetChild(0).gameObject);
#else
                        Destroy(transform.GetChild(0).gameObject);

#endif
        }
    }

    [ContextMenu(nameof(PrepareLevel))]
    private void PrepareLevel()
    {
        DeleteLevel();
        for (int i = 0; i < prefabIndexes.Length; i++)
        {
            var tmp = CreateObject(prefabIndexes[i]);
            SetObjectPosition(tmp, prefabIndexes[i], i);
        }
    }


    private void SetObjectPosition(GameObject tmp, int PrefabNumber, int SortingNumber)
    {
        tmp.transform.localPosition = new Vector3(offsets[PrefabNumber].x, offsets[PrefabNumber].y,
            ((barieroffset * SortingNumber) + startoffset) +
            offsets[PrefabNumber].z);
    }

    private GameObject CreateObject(int PrefabNumber)
    {
        var tmp = Instantiate(_prefabs[PrefabNumber], transform.position, Quaternion.Euler(rotationVector),
            transform);
        return tmp;
    }

    private void CreatePrefab(int i)
    {
        var tmp = Instantiate(_prefabs[i], transform.position, Quaternion.Euler(rotationVector),
            transform);
        tmp.transform.localPosition = new Vector3(offsets[i].x, offsets[i].y,
            ((barieroffset * i) + startoffset) +
            offsets[i].z);
    }

}