using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Color colorAlive;
    [SerializeField] private Color colorDead;
    [SerializeField] private Renderer render;
    [SerializeField] private MaterialPropertyBlock block;
    [SerializeField] protected int materialIndex;
    [SerializeField] private ParticleSystem bloodFX;
    void Awake()
    {
        block = new MaterialPropertyBlock();
    }
    public void SetAliveColor() => UpdateColor(colorAlive);
    public void SetDeadColor() => UpdateColor(colorDead);
    public void PlayFX() => bloodFX.Play();
    private void UpdateColor(Color color)
    {
        render.GetPropertyBlock(block, materialIndex);
        block.SetColor("_Color", color);
        render.SetPropertyBlock(block);
    }
}
