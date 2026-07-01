using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponPhysics : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public Vector3 Velocity => rb.velocity;

    public void Launch(Vector3 dir, float force)
    {
        rb.isKinematic = false;
        rb.velocity = dir * force;
    }

    public void ApplyGravity(float scale)
    {
        rb.velocity += Vector3.down * scale * Time.fixedDeltaTime;
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void SetKinematic(bool value) => rb.isKinematic = value;
}
