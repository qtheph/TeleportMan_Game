using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilDrumHit : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public float Radius;
    public void DetechEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, Radius, layerMask);
        foreach (var hit in hits)
        {
            ITakeDamage target = hit.GetComponentInParent<ITakeDamage>();
            if (target != null)
            {
                target.TakeDamage();
                AudioManager.Instance.BoomFX();
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
