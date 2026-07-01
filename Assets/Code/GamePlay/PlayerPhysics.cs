using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public void SetKinematic(bool value) => rb.isKinematic = value;
    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    public void SetVelocity(Vector3 velocity) => rb.velocity = velocity;
    public void ApplyGravity(float scale)
    {
        rb.velocity += Vector3.down * scale * Time.fixedDeltaTime;
    }

}
