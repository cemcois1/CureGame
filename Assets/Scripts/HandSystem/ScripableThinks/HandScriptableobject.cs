using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Hand",menuName = "HandConfig")]
public class HandScriptableobject : ScriptableObject
{
    public float localJumpDuration= 1f;
    public float localJumpPower= 2f;
    public float offset = .4f;
    public float startOffset = 1f;
    public Vector3 dishInstantateRotation;
    public int DishCollectingMoney=4;
}
