using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class BossNinja : EnemyBase
{
    [SerializeField] Transform handWeapon;
    [SerializeField] Transform handrop;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int health;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private Transform[] teleportPoints;
    int index = 0;
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
        health--;
        StartCoroutine(WaitForDisableCol());

        if (health <= 0)
        {
            base.Die();
            handWeapon.SetParent(handrop);
        }

    }
    private void EnableCol(bool state)
    {
        colliders = GetComponentsInChildren<Collider>();
        foreach (var col in colliders)
        {
            col.enabled = state;
        }
    }
    IEnumerator WaitForDisableCol()
    {
        EnableCol(false);
        if (health > 0) Scale();
        yield return new WaitForSeconds(2f);
        EnableCol(true);
    }
    private void Scale()
    {
        Vector3 scaleSize = transform.localScale;
        transform.DOScale(Vector3.zero, .25f).OnComplete(() =>
        {
            Teleport();
            transform.DOScale(scaleSize, .25f);
        });
    }
    private void Teleport()
    {
        transform.position = teleportPoints[index].position;
        index++;
        if (index >= teleportPoints.Length) index = 0;
    }
}
