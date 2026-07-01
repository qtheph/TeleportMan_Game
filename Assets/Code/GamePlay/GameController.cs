using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    //[SerializeField] private LevelManager level;
    [SerializeField] private InputController input;
    [SerializeField] private DragController drag;
    [SerializeField] private ArrowVisual arrow;
    [SerializeField] private PlayerWeaponBase playerWeapon;
    [SerializeField] private PlayerBase player;
    void OnEnable()
    {
        input.OnClick += HandleClick;
        input.OnHolding += HandleHolding;
        input.OnRelease += HandleRelease;

    }
    void OnDisable()
    {
        input.OnClick -= HandleClick;
        input.OnHolding -= HandleHolding;
        input.OnRelease -= HandleRelease;

    }
    private void HandleClick()
    {
        if (GameManager.Instance.CurrentState != GameState.Play) return;
        //GameManager.Instance.ShowUI2(false);
        TimeController.Instance.SetSlow(0.25f);
        TimeController.Instance.SyncPhysic(0.25f);

        arrow.Hide();
        playerWeapon.Clicking();
        player.Clicking();

    }
    private void HandleHolding(Vector3 dragPos)
    {
        if (GameManager.Instance.CurrentState != GameState.Play) return;
        //Debug.Log("DragPos" + dragPos);
        if (dragPos == Vector3.zero)
        {
            arrow.Hide();
            return;
        }
        arrow.Show();
        drag.UpdateDrag(dragPos);
        arrow.UpdateVisual(player.transform.position, drag.Direction, drag.Power);
    }
    private void HandleRelease()
    {
        if (GameManager.Instance.CurrentState != GameState.Play) return;
        //Cancel slow time on release
        TimeController.Instance.ResetNormal();
        //Sync Physics
        TimeController.Instance.ResetPhysic();
        arrow.Hide();
        playerWeapon.Throw(drag.Direction, drag.Power);
        player.Throw(drag.Direction);
    }
}

