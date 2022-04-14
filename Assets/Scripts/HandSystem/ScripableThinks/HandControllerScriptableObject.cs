using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "HandController", menuName = "HandControllerConfig")]
public class HandControllerScriptableObject : ScriptableObject
{
    [FormerlySerializedAs("swipeDeltaPassingLimit")] public float DeltaPassingLimit = 1;
    public float swipeingTime = 1f;
}