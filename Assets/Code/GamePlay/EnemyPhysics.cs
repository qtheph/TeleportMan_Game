using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected Rigidbody[] rigidbodies;
    [SerializeField] protected Collider[] colliders;
    public void SetChildKinematic(bool state)
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = state;
        }
    }
    public void SetChildTrigger(bool state)
    {
        colliders = GetComponentsInChildren<Collider>();
        foreach (var col in colliders)
        {
            col.isTrigger = state;
        }
    }
}
