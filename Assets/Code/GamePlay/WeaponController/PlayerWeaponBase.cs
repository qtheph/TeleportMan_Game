using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public abstract class PlayerWeaponBase : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSO playerWeaponSO;
    [SerializeField] private PlayerWeaponPhysics playerWeaponPhysics;
    [SerializeField] private PlayerWeaponDetechGround playerWeaponDetechGround;
    [SerializeField] private ZPos zPos;
    [SerializeField] private float radius;
    [SerializeField] private Vector3 offset;
    public enum WeaponState
    {
        Clicking,
        Throwing
    }
    bool isUseGravity = false;
    public WeaponState currState = WeaponState.Clicking;
    public void ChangeState(WeaponState newState)
    {
        if (currState == newState) return;
        currState = newState;
        switch (currState)
        {
            case WeaponState.Clicking:
                isUseGravity = false;
                break;
            case WeaponState.Throwing:
                isUseGravity = true;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        playerWeaponDetechGround.CheckGround(transform.position + playerWeaponSO.offsetRadiusPoint, playerWeaponSO.radius, playerWeaponSO.offset);
    }
    void FixedUpdate()
    {
        if (isUseGravity)
        {
            playerWeaponPhysics.ApplyGravity(3f);
        }
    }
    public void Throw(Vector3 dir, float power)
    {
        ChangeState(WeaponState.Throwing);
        playerWeaponPhysics.ResetVelocity();
        float force = Mathf.Lerp(playerWeaponSO.minForce, playerWeaponSO.maxForce, power);
        playerWeaponPhysics.Launch(dir, force);
        zPos.UpdateZPos(12f);
        LookDir(dir);
    }
    public void Clicking()
    {
        if (playerWeaponDetechGround.IsGround)
        {
            transform.position = playerWeaponDetechGround.HitPoint + playerWeaponDetechGround.Dir * playerWeaponSO.offsetOnGround;
        }

        zPos.UpdateZPos(11f);

        ChangeState(WeaponState.Clicking);
        playerWeaponPhysics.SetKinematic(false);
    }

    private void LookDir(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
    public void InitWeapon(Transform spawnPoint)
    {
        StartCoroutine(ResetWeaponVelocity(spawnPoint));
    }
    private void ResetPos(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }
    private IEnumerator ResetWeaponVelocity(Transform spawnPoint)
    {
        yield return new WaitForEndOfFrame();
        ChangeState(WeaponState.Clicking);
        playerWeaponPhysics.ResetVelocity();
        playerWeaponPhysics.SetKinematic(true);
        ResetPos(spawnPoint);
        zPos.UpdateZPos(11f);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + playerWeaponSO.offsetRadiusPoint, playerWeaponSO.radius);
    }
}
