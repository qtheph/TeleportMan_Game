using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public enum CameraState
{
    FollowingPlayer,
    ViewingBoss,
    ReturningToPlayer
}
public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject panelClick;
    [SerializeField] Transform playerTarget;
    [SerializeField] Transform bossTarget;
    [SerializeField] float smoothTime;
    [SerializeField] float minClampX;
    [SerializeField] float maxClampX;
    [SerializeField] float minClampY;
    [SerializeField] float maxClampY;
    public Vector3 offsetBoss;
    public Vector3 offset;
    private Vector3 velocity;
    public CameraState currState = CameraState.FollowingPlayer;
    void Start()
    {
        offset = transform.position - playerTarget.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch (currState)
        {
            case CameraState.FollowingPlayer:
                FollowPlayer();
                break;
        }
    }
    public void ViewBoss(Transform target)
    {
        bossTarget = target;
        if (bossTarget == null) return;
        panelClick.SetActive(false);
        GameManager.Instance.CurrentState = GameState.Pause;

        currState = CameraState.ViewingBoss;
        Vector3 targetPos = bossTarget.position + offsetBoss;

        transform.DOMove(targetPos, 2f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                StartCoroutine(ReturnPlayer());
            });
    }
    IEnumerator ReturnPlayer()
    {
        yield return new WaitForSeconds(1.5f);

        currState = CameraState.ReturningToPlayer;
        Vector3 returnPos = playerTarget.position + offset;
        transform.DOMove(returnPos, 2f).SetEase(Ease.InOutSine).OnComplete(() =>
       {
           currState = CameraState.FollowingPlayer;
           panelClick.SetActive(true);
       });
    }
    public void FollowPlayer()
    {
        if (playerTarget == null) return;
        Vector3 targetPos = playerTarget.position + offset;
        targetPos = ClampPositon(targetPos);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
    private Vector3 ClampPositon(Vector3 targetPos)
    {
        targetPos.x = Mathf.Clamp(targetPos.x, minClampX, maxClampX);
        targetPos.y = Mathf.Clamp(targetPos.y, minClampY, maxClampY);
        return targetPos;
    }
}
