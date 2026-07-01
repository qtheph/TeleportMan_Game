using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken_E : EnemyWeaponDetech
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float zPos;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float timeLife;
    public float timer;
    Vector3 dir;
    public void Init()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        Vector3 pos = transform.position;
        pos.z = zPos;
        transform.position = pos;
        dir = (player.position - transform.position).normalized;
        timer = 0f;
    }
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
        transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0);
        timer += Time.deltaTime;
        if (timer > timeLife)
        {
            ReturnPool();
        }
    }
    // public override void OnHitPlayer(PlayerBase player)
    // {
    //     base.OnHitPlayer(player);
    //     ReturnPool();
    // }
    private void ReturnPool()
    {
        ObjectPooling.Instance.ReturnPool(this);
    }
}

