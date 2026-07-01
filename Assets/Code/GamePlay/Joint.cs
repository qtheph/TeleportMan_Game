using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
    FixedJoint fixedJoint;
    public void StartJoint(Rigidbody targetRb)
    {
        if (fixedJoint != null) return;
        fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = targetRb;
    }
    public void UnJoint()
    {
        if (fixedJoint == null) return;
        Destroy(fixedJoint);
        fixedJoint = null;
    }
}
