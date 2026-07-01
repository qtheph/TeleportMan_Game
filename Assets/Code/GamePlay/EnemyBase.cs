using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, ITakeDamage
{
    // Start is called before the first frame update
    // public static Action<EnemyBase> OnEnemyDead;
    public static Action OnEnemyDead;
    [SerializeField] protected EnemyVisual enemyVisual;
    [SerializeField] protected EnemyPhysics enemyPhysics;
    [SerializeField] protected EnemyAnimator enemyAnimator;
    [SerializeField] private int deadLayer;
    public enum EnemyState
    {
        Alive,
        Dead
    }
    protected EnemyState currState = EnemyState.Alive;

    protected virtual void Start()
    {
        enemyVisual.SetAliveColor();
        enemyPhysics.SetChildKinematic(true);
        enemyPhysics.SetChildTrigger(true);
    }
    protected virtual void Update()
    {
        if (IsDead()) return;
        OnAliveUpdate();

    }
    protected abstract void OnAliveUpdate();
    public virtual void Die()
    {
        if (IsDead()) return;
        currState = EnemyState.Dead;
        enemyVisual.SetDeadColor();
        enemyVisual.PlayFX();
        enemyPhysics.SetChildKinematic(false);
        enemyPhysics.SetChildTrigger(false);
        enemyAnimator.EnableAnimator(false);
        ChangeLayer();
        OnEnemyDead?.Invoke();
        //OnEnemyDead?.Invoke(this);
    }
    public bool IsDead() => currState == EnemyState.Dead;

    public void TakeDamage()
    {
        if (IsDead()) return;
        Die();
    }
    void ChangeLayer()
    {
        gameObject.layer = deadLayer;
        Collider[] childColliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider col in childColliders)
        {
            col.gameObject.layer = deadLayer;
        }
    }
}
