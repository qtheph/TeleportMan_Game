using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowShuriken : MonoBehaviour
{
    [SerializeField] Transform handPoint;
    public void Throw()
    {
        Shuriken_E shuriken = ObjectPooling.Instance.Get();
        shuriken.transform.position = handPoint.position;
        shuriken.transform.rotation = Quaternion.Euler(0, 90, 0);
        shuriken.Init();
    }
}
