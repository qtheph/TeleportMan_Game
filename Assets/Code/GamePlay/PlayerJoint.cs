using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoint : MonoBehaviour
{
    [SerializeField] Rigidbody weaponRb;
    FixedJoint fixedJoint;
    void Start()
    {
    }
    public void Holding()
    {
        if (gameObject.GetComponent<FixedJoint>() == null)
        {
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = weaponRb;
        }
    }
    public void Release()
    {
        if (fixedJoint != null)
        {
            Destroy(fixedJoint);
            fixedJoint = null;
        }
    }
}
