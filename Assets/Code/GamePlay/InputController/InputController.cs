using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action OnClick;
    public event Action<Vector3> OnHolding;
    public event Action OnRelease;

    private Vector3 startPos;
    private Vector3 currPos;
    public Vector3 DragPos { get; private set; }

    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startPos = GetMousePos();
            OnClick?.Invoke();
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            currPos = GetMousePos();
            if (currPos == startPos) return;

            DragPos = currPos - startPos;
            OnHolding?.Invoke(DragPos);

        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            OnRelease?.Invoke();
        }
    }
    private Vector3 GetMousePos()
    {
        // Chuyển tọa độ màn hình (pixel) ➜ tọa độ chuẩn hoá (0 → 1) Không phụ thuộc độ phân giải
        return Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }
}
