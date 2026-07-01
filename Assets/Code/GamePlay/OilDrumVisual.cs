using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilDrumVisual : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] private GameObject oilDrumMesh;
    public void Active(bool state)
    {
        oilDrumMesh.SetActive(state);
    }
    public void PlayFX()
    {
        explosionFX.Play();
    }
}
