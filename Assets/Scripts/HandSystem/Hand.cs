using System;
using System.Collections.Generic;
using Developing.Scripts.HandSystem;
using Developing.Scripts.HandSystem.GateSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum HandPos
{
    Left,
    Right
}

public class Hand : MonoBehaviour
{
    public bool isPunchScaleable = true;
    public GameObject Money;

    [SerializeField] public List<Dish> holdingDishes = new List<Dish>();
    [SerializeField] private HandPos position;
    [SerializeField] private Hand otherHand;

    [SerializeField] private TextMeshPro text;
    [SerializeField] private TextMeshPro moneyCountTextMeshPro;
    [SerializeField] private Transform garbageParent;
    [SerializeField] private Transform dishParent;

    [Header("For Dish Initialization")] [SerializeField]
    private GameObject Dish;

    [Header("For Config file")] [SerializeField]
    private HandScriptableobject config;

    [SerializeField] private Camera gameplayCamera;
    [SerializeField] private int counter;

    private void OnEnable()
    {
        UpdateDishCountText();
    }

    private void RemoveItem()
    {
        if (holdingDishes.Count < 1) return;
        var throwingDish = holdingDishes[holdingDishes.Count - 1];
        holdingDishes.Remove(throwingDish);
        ///stop pulsing
        throwingDish.gameObject.GetComponent<PulseEffect>().enabled = false;
        ///push object
        throwingDish.transform.parent = Garbageholder.instance.transform;
        var rbDish = throwingDish.gameObject.AddComponent<Rigidbody>();
        var frontForce = 880;
        if (transform.position.x < 0)
        {
            rbDish.AddForce(Vector3.forward * frontForce + Vector3.right * 200 * -Random.Range(0f, 1f));
        }
        else
        {
            rbDish.AddForce(Vector3.forward * frontForce + Vector3.right * 200 * Random.Range(0f, 1f));
        }


        /*
        var tmp = DOTween.Sequence();
        tmp.Append(throwingDish.transform.DOPunchScale(throwingDish.transform.localScale*0.9f, .1f));
        tmp.Append(throwingDish.transform.DOScale(Vector3.zero, .2f));
        tmp.OnComplete(() => Destroy(throwingDish));
        */
        UpdateDishCountText();
    }

    public void PushItem()
    {
        if (holdingDishes.Count < 1) return;

        //print(holdingDishes[holdingDishes.Count-1].name);
        var throwingDish = holdingDishes[holdingDishes.Count - 1];
        holdingDishes.Remove(throwingDish);
        throwingDish.transform.DOLocalJump(otherHand.GetAddingPosition(otherHand.transform.GetChild(0).localPosition.x),
            config.localJumpPower, 1, config.localJumpDuration);

        otherHand.addToList(throwingDish);
        UpdateDishCountText();
        SetPulsingValue(throwingDish, otherHand);
        otherHand.UpdateDishCountText();
    }

    private void addToList(Dish dish)
    {
        switch (position)
        {
            case HandPos.Left:
                holdingDishes.Add(dish);
                break;
            case HandPos.Right:
                holdingDishes.Add(dish);
                //holdingDishes.Insert(0, dish);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CollectibleDish collectibleDish))
        {
            InstantateDishArray(collectibleDish);
            Destroy(other.gameObject);
        }

        if (other.TryGetComponent(out IGate gate))
        {
            gate.CollideWithObject(this);
        }
    }

    public void InstantateDishArray(int addingCount)
    {
        if (addingCount > 0)
        {
            for (int i = 0; i < addingCount; i++)
            {
                GameObject obj;
                if (holdingDishes.Count > i)
                {
                    var dish = InstantateDish(this, holdingDishes[i].sweetType, holdingDishes[i].isCleaned);
                    IncreaseMoney(config.DishCollectingMoney, dish.transform.position);
                    SetPulsingValue(dish, this);
                }
                else
                {
                    var dish = InstantateDish(this);
                    IncreaseMoney(config.DishCollectingMoney, dish.transform.position);
                    SetPulsingValue(dish, this);
                }
            }
        }
        else
        {
            for (int i = 0; i < -addingCount; i++)
            {
                RemoveItem();
            }
        }

        UpdateDishCountText();
    }

    private void SetPulsingValue(Dish dish, Hand holdingHand)
    {
        PulseEffect pulseEffect = dish.GetComponent<PulseEffect>();
        pulseEffect.currentspeed = pulseEffect.speed * (float) Math.Pow(holdingHand.holdingDishes.Count, 1f / 4f);
    }

    public void UpdateDishCountText()
    {
        text.text = holdingDishes.Count.ToString();
    }

    private void InstantateDishArray(CollectibleDish collectibleDish)
    {
        if (collectibleDish.addingCount > 0)
        {
            for (int i = 0; i < collectibleDish.addingCount; i++)
            {
                var dish = InstantateDish(this);
                IncreaseMoney(config.DishCollectingMoney);
                SetPulsingValue(dish, this);
            }
        }
        else
        {
            for (int i = 0; i < -collectibleDish.addingCount; i++)
            {
                RemoveItem();
            }
        }

        UpdateDishCountText();
    }

    private Dish InstantateDish(Hand hand, SweetType sweetType = SweetType.empty, bool isClean = false)
    {
        var localPosition = transform.GetChild(0).localPosition;

        var obj = Instantiate(Dish, GetAddingPosition(localPosition.x),
            Quaternion.Euler(Vector3.zero),
            dishParent);
        obj.transform.localPosition = GetAddingPosition(localPosition.x);
        obj.transform.localRotation = Quaternion.Euler(config.dishInstantateRotation);
        Dish dish = obj.GetComponent<Dish>();
        if (isClean) dish.CleanDish();
        dish.AddSweet(sweetType);
        this.addToList(dish);
        return dish;
    }

    private Vector3 GetAddingPosition(float parentLocalPositionX = 3.5f)
    {
        Vector3 valueX = position switch
        {
            HandPos.Left => Vector3.right * parentLocalPositionX,
            HandPos.Right => Vector3.right * parentLocalPositionX,
        };
        var UpSideValue = (Vector3.up * config.startOffset) + (holdingDishes.Count * config.offset * Vector3.up);
        return UpSideValue + valueX;
    }

    public void RemoveDish(Hand hand, Vector3 targetposition, float duration = 1f)
    {
        if (holdingDishes.Count < 1) return;
        var throwingDish = holdingDishes[holdingDishes.Count - 1];
        holdingDishes.Remove(throwingDish);
        if (duration != 0)
        {
            throwingDish.transform.DOMove(targetposition, duration);
        }

        throwingDish.transform.parent = garbageParent;
        throwingDish.transform.rotation = Quaternion.Euler(Vector3.zero);
        throwingDish.GetComponent<PulseEffect>().enabled = false;
        //throwingDish.transform.DOScale(Vector3.zero, .2f).OnComplete(() => Destroy(throwingDish));
        UpdateDishCountText();
    }

    public void SellDish(int moneyAddingCount)
    {
        if (holdingDishes.Count > 0)
        {
            IncreaseMoney(moneyAddingCount);
        }
    }

    public void IncreaseMoney(int moneyAddingCount, Vector3 worldPos = new Vector3())
    {
        Vector3 DishWorldPosition;
        if (worldPos.Equals(Vector3.zero))
        {
            DishWorldPosition = holdingDishes[holdingDishes.Count - 1].transform.position;
        }
        else
        {
            DishWorldPosition = worldPos;
        }

        /*var moneyObject = Instantiate(Money, gameplayCamera.WorldToScreenPoint(DishWorldPosition),
            Quaternion.Euler(Vector3.zero),
            UIManager.instance.Moneyholder);*/
        var addingCount = (int.Parse(moneyCountTextMeshPro.text) + moneyAddingCount);
        UIManager.instance.MoneyCount = addingCount;
        moneyCountTextMeshPro.text = addingCount.ToString();
    }

    [ContextMenu(nameof(CreateNewObjectAndBindBalancedObjects))]
    public void CreateNewObjectAndBindBalancedObjects()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5f, 0);
        cube.GetComponent<MeshRenderer>().enabled = false;
        cube.transform.parent = transform.GetChild(0).parent;
        cube.transform.position = transform.GetChild(0).position;
        transform.GetChild(0).GetComponent<BalanceObjects>().referanceObject = cube.transform;
    }
}