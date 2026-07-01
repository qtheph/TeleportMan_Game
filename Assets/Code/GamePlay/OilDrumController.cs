using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilDrumController : MonoBehaviour
{
    public enum OilDrumState
    {
        Idle,
        Exploded
    }
    public OilDrumState currState = OilDrumState.Idle;
    [SerializeField] private OilDrumVisual oilDrumVisual;
    [SerializeField] private OilDrumHit oilDrumHit;

    void OnTriggerEnter(Collider other)
    {
        if (currState == OilDrumState.Exploded) return;
        Explode();
    }

    void Explode()
    {
        currState = OilDrumState.Exploded;
        oilDrumVisual.Active(false);
        oilDrumVisual.PlayFX();
        oilDrumHit.DetechEnemy();
        Destroy(gameObject, 2f);
    }
}
