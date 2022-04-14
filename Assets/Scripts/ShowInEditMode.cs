using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

public class ShowInEditMode : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        var component = GetComponent<BoxCollider>();
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position+component.center, component.size);
    }
}
#endif
