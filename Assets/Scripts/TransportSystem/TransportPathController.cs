using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class TransportPathController : MonoBehaviour
{
    [SerializeField] private Vector3 transportDirection;
    private List<Rigidbody> moveableTransforms = new List<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IMoveable>(out IMoveable moveable))
        {
            moveableTransforms.Add(other.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (Rigidbody moveable in moveableTransforms)
        {
            moveable.MovePosition(moveable.position + (transportDirection * Time.deltaTime));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IMoveable>(out IMoveable moveable))
            moveableTransforms.Remove(other.GetComponent<Rigidbody>());
    }
}