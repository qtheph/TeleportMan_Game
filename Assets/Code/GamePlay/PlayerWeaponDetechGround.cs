using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDetechGround : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    public Vector3 Dir { get; private set; }
    public Vector3 HitPoint { get; private set; }
    public bool IsGround { get; private set; }

    public void CheckGround(Vector3 origin, float radius, Vector3 offsetRadiusPoint)
    {
        Collider[] hits = Physics.OverlapSphere(origin + offsetRadiusPoint, radius, groundMask);
        IsGround = hits.Length > 0;
        if (IsGround)
        {

            Vector3 swordPos = origin;
            HitPoint = hits[0].ClosestPoint(swordPos);
            Dir = (swordPos - HitPoint).normalized;
            //if (HitPoint == Vector3.zero) HitPoint = Vector3.up;
            Debug.DrawLine(swordPos, HitPoint, Color.yellow, 5.0f);
            Debug.DrawRay(HitPoint, Dir * 1.5f, Color.green, 5.0f);
            //Debug.Log("Detechhhhhhhhhh");
        }
    }
}
