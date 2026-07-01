using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyWeaponDetech : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.CurrentState == GameState.GameOver) return;
        ITakeDamage target = other.GetComponentInParent<ITakeDamage>();
        if (target != null)
        {
            if (!target.IsDead())
            {
                target.TakeDamage();
            }
        }
    }
}
