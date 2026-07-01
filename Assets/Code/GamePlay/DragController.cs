using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float maxDragDist;
    public float Power { get; private set; }
    public Vector3 Direction { get; private set; }
    public void UpdateDrag(Vector3 dragPos)
    {
        Direction = dragPos.normalized;
        Power = Mathf.Clamp01(dragPos.magnitude / maxDragDist);
    }
}
