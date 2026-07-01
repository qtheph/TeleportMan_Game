using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MeleeEnemy : EnemyBase
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] Transform weapon;
    [SerializeField] Transform handrop;

    protected override void OnAliveUpdate()
    {
        enemyAnimator.SetBool("MeleeAttack", false);
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, targetLayerMask);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                enemyAnimator.SetBool("MeleeAttack", true);
            }
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
        weapon.SetParent(handrop);
    }
}
