using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float sqrMagnitudeVelocity;
    [SerializeField] private float rotateSpeed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.sqrMagnitude > sqrMagnitudeVelocity)
        {
            Rotate();
        }
    }
    public void Rotate()
    {
        transform.Rotate(new Vector3(0f, rotateSpeed * rb.velocity.magnitude * Time.deltaTime, 0f));
    }
}
