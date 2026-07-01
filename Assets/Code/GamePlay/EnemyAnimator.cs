using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    public void EnableAnimator(bool state) => animator.enabled = state;
    public void SetBool(string name, bool state) => animator.SetBool(name, state);
}
