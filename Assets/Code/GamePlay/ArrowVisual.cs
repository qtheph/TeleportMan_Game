using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ArrowVisual : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float minScale;
    [SerializeField] float maxScale;
    [SerializeField] Transform player;
    [SerializeField] GameObject arrow;
    [SerializeField] Vector3 offset;
    // Start is called before the first frame update
    public void Show() => arrow.SetActive(true);
    public void Hide() => arrow.SetActive(false);
    public void UpdateVisual(Vector3 origin, Vector3 dir, float power)
    {
        ArrowPosition(origin, dir);
        ArrowLookAtDir(dir);
        ScaleArrow(power);
    }
    private void ArrowPosition(Vector3 origin, Vector3 dir)
    {
        Vector3 currentPos = origin + offset + dir * radius;
        transform.position = new Vector3(currentPos.x, currentPos.y, transform.position.z);
    }
    private void ArrowLookAtDir(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
    private void ScaleArrow(float power)
    {
        float scaleY = Mathf.Lerp(minScale, maxScale, power);
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position + offset, radius);
    }
}
