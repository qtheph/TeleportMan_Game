using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] ParticleSystem appearFX;
    [SerializeField] GameObject teleportFX;
    public void PlayAppearFX()
    {
        appearFX.Play();
    }
    public void PlayTeleportFX(Vector3 pos)
    {
        GameObject teleport = Instantiate(teleportFX, pos, Quaternion.identity);
        Destroy(teleport, .5f);
    }
}
