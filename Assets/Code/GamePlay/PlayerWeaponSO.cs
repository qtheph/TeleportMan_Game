using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponSO", menuName = "Game/PlayerWeaponSO")]
public class PlayerWeaponSO : ScriptableObject
{
    public float radius;
    public float minForce;
    public float maxForce;
    public float gravityScale;
    public Vector3 offset;
    public Vector3 offsetRadiusPoint;
    public float offsetOnGround;

}
