using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class PlayerBase : MonoBehaviour, ITakeDamage
{
    public event Action OnPlayerDead;
    //public event Action OnPlayerAlive;
    [SerializeField] protected PlayerPhysics physics;
    [SerializeField] protected PlayerVisual visual;
    [SerializeField] private PlayerFX playerFX;
    [SerializeField] protected ZPos zPos;
    [SerializeField] protected Joint joint;
    [SerializeField] private Rigidbody weaponRb;
    [SerializeField] private Transform weaponGridPoint;

    [SerializeField] float gravityScale;
    [SerializeField] float speedReverse;
    bool isUseGravity = false;

    public enum PlayerState
    {
        Idle,
        Clicking,
        Holding,
        Throwing,
        Dead
    }
    public PlayerState currState = PlayerState.Idle;

    protected virtual void Start()
    {

    }
    protected virtual void FixedUpdate()
    {
        if (isUseGravity) physics.ApplyGravity(gravityScale);
    }

    public virtual void ChangeState(PlayerState newState)
    {
        if (currState == newState) return;
        currState = newState;
        switch (currState)
        {
            case PlayerState.Idle:
                isUseGravity = false;
                break;
            case PlayerState.Clicking:
                isUseGravity = false;
                break;
            case PlayerState.Holding:
                isUseGravity = true;
                break;
            case PlayerState.Throwing:
                isUseGravity = true;
                break;
            case PlayerState.Dead:
                isUseGravity = true;
                break;
        }
    }

    public virtual void Clicking()
    {
        if (IsDead()) return;
        if (currState == PlayerState.Clicking) return;
        ChangeState(PlayerState.Clicking);

        physics.SetKinematic(false);

        Vector3 lastPoint = transform.position;
        transform.position = weaponGridPoint.position;

        playerFX.PlayAppearFX();
        playerFX.PlayTeleportFX(lastPoint);


        physics.ResetVelocity();
        physics.SetVelocity(weaponRb.velocity);
        joint.StartJoint(weaponRb);

    }
    public virtual void Holding()
    {
        if (IsDead()) return;
        if (currState == PlayerState.Holding) return;
        ChangeState(PlayerState.Holding);
        physics.SetVelocity(weaponRb.velocity);
    }

    public virtual void Throw(Vector3 dir)
    {
        if (IsDead()) return;
        if (currState == PlayerState.Throwing) return;
        ChangeState(PlayerState.Throwing);
        physics.ResetVelocity();
        Vector3 throwForce = -dir * speedReverse * weaponRb.velocity.magnitude;
        physics.SetVelocity(throwForce);
        joint.UnJoint();
    }
    // public virtual void UpdateOnGroundPos()
    // {
    //     Vector3 pos = transform.position;
    //     pos += offsetOnGround;
    //     transform.position = pos;
    // }
    // public virtual void UpdateUnderGroundPos()
    // {
    //     Vector3 pos = transform.position;
    //     pos += -offsetOnGround;
    //     transform.position = pos;
    // }
    // public void SetGravityHolding()
    // {
    //     this.scale = scaleGravityHolding;
    // }
    public void Die()
    {
        if (IsDead()) return;
        ChangeState(PlayerState.Dead);
        visual.SetDeadColor();
        joint.UnJoint();
        OnPlayerDead?.Invoke();
    }

    public void InitPlayer(Transform spawnPoint)
    {
        ResetPos(spawnPoint);
        ResetPlayerState();
        visual.SetAliveColor();
        //OnPlayerAlive?.Invoke();
    }
    private void ResetPos(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }
    private void ResetPlayerState()
    {
        ChangeState(PlayerState.Idle);
        physics.SetKinematic(true);
        physics.ResetVelocity();
        zPos.UpdateZPos(12f);
    }

    public void TakeDamage()
    {
        if (IsDead()) return;
        Die();
    }
    public bool IsDead() => currState == PlayerState.Dead;
}
