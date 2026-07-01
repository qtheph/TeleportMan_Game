using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] protected Renderer render;
    [SerializeField] int materialIndex;
    [SerializeField] private Color colorAlive;
    [SerializeField] private Color colorDead;
    [SerializeField] protected MaterialPropertyBlock block;

    public void SetDeadColor() => UpdateColor(colorDead);
    public void SetAliveColor() => UpdateColor(colorAlive);

    private void UpdateColor(Color color)
    {
        block = new MaterialPropertyBlock();
        render.GetPropertyBlock(block, materialIndex);
        block.SetColor("_Color", color);
        render.SetPropertyBlock(block);
    }
}
