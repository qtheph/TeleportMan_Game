using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : EnemyBase
{
    [SerializeField] Transform handrop;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float waitTime;
    [SerializeField] Collider shield;
    [SerializeField] Transform handWeapon;
    private Transform targetPos;
    private Quaternion targetRot;
    private bool isWaiting = false;
    private bool isRotating = false;

    protected override void Start()
    {
        base.Start();
        shield.isTrigger = false;
        targetPos = pointB;
        SetRotateAngle();
    }
    protected override void OnAliveUpdate()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, targetRot) < 0.1f)
            {
                transform.rotation = targetRot;
                isRotating = false;
            }
            return;
        }
        if (isWaiting) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);
        enemyAnimator.SetBool("ShieldWalking", true);

        if (Vector3.Distance(transform.position, targetPos.position) < 0.1f)
        {
            enemyAnimator.SetBool("ShieldWalking", false);
            StartCoroutine(WaitingToChangeTarget());
        }
    }

    IEnumerator WaitingToChangeTarget()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        targetPos = (targetPos == pointA) ? pointB : pointA;
        SetRotateAngle();
        isRotating = true;
        isWaiting = false;
    }

    private void SetRotateAngle()
    {
        float rotateAngle = (targetPos == pointA) ? -90f : 90f;
        targetRot = Quaternion.Euler(0, rotateAngle, 0);
    }
    public override void Die()
    {
        base.Die();
        handWeapon.SetParent(handrop);
    }
}
