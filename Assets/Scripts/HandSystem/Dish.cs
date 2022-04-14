using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
[SelectionBase]
public class Dish : MonoBehaviour
{
    [SerializeField] private GameObject[] dirts;
    [SerializeField] private GameObject[] sweets;

    public MeshRenderer dishMesh;

    public bool isCleaned = false;
    public SweetType sweetType = SweetType.empty;

    private void OnEnable()
    {
        SmoothStart();
    }

    public void SmoothStart()
    {
        SetDirtyTransform();
        var transformLocalScale = transform.localScale;
        transform.DOScale(transformLocalScale * 1.5f, .2f).SetEase(Ease.InOutBack)
            .OnComplete(() => transform.DOScale(transformLocalScale, .1f));
    }

    [ContextMenu(nameof(SetDirtyTransform))]
    private void SetDirtyTransform()
    {
        foreach (var dirt in dirts)
        {
            dirt.transform.localRotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));
        }
    }

    public void DishPolishEffect(Color targetColor, float duration)
    {
        dishMesh.material.DOColor(targetColor, duration).From();
    }

    public void AddSweet(SweetType numberofSweet)
    {
        sweetType = numberofSweet;
        int something = (int) numberofSweet;
        if (something > sweets.Length)
        {
            Debug.LogError("Error You dont got this sweet");
        }

        foreach (var sweet in sweets)
        {
            sweet.SetActive(false);
        }

        sweets[something].SetActive(true);
    }

    public void CleanDish()
    {
        foreach (var dirt in dirts)
        {
            dirt.SetActive(false);
        }

        isCleaned = true;
    }

    public void DirtyDish()
    {
        isCleaned = false;
        foreach (var dirt in dirts)
        {
            dirt.SetActive(true);
        }
    }
}