using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDetech : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.CurrentState == GameState.GameOver) return;
        ITakeDamage target = other.GetComponentInParent<ITakeDamage>();
        if (target != null)
        {
            if (!target.IsDead())
            {
                target.TakeDamage();
                AudioManager.Instance.EnemyDie();
                StartCoroutine(TimeController.Instance.SetSlowOnKill(0.25f));
            }
        }
    }

}
