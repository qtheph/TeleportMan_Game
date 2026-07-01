using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField] Transform handWeapon;
    [SerializeField] Transform handrop;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rotateSpeed;
    bool isRotating = false;
    Quaternion targetRot;
    protected override void OnAliveUpdate()
    {
        enemyAnimator.SetBool("RangedAttack", false);
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Player")) continue;
            if (!isRotating)
            {
                Vector3 dirToPlayer = hit.transform.position - transform.position;
                float rotY = dirToPlayer.x > 0 ? 90 : -90;
                targetRot = Quaternion.Euler(0, rotY, 0);
                if (Quaternion.Angle(transform.rotation, targetRot) > 0.1f)
                {
                    isRotating = true;
                }
            }
            if (isRotating)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
                if (Quaternion.Angle(transform.rotation, targetRot) < 0.1f)
                {
                    transform.rotation = targetRot;
                    isRotating = false;
                }
            }
            enemyAnimator.SetBool("RangedAttack", true);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public override void Die()
    {
        base.Die();
        handWeapon.SetParent(handrop);
    }
}
