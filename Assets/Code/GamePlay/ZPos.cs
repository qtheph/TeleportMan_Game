using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPos : MonoBehaviour
{
    public void UpdateZPos(float zPos)
    {
        Vector3 pos = transform.localPosition;
        pos.z = zPos;
        transform.localPosition = pos;
    }
}
