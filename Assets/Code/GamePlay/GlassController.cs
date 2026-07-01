using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassController : MonoBehaviour, ITakeDamage
{
    public enum GlassState
    {
        idle,
        broke
    }
    private GlassState currState = GlassState.idle;
    [SerializeField] private GameObject glassNormal;
    [SerializeField] private GameObject breakGlass;

    public bool IsDead()
    {
        return currState == GlassState.broke;
    }

    public void TakeDamage()
    {
        if (IsDead()) return;
        currState = GlassState.broke;
        glassNormal.SetActive(false);
        breakGlass.SetActive(true);
        Destroy(gameObject, 2);
    }
}
