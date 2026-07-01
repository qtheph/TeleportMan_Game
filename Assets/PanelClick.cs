using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CanvasGroup panel;
    [SerializeField] private UIManager UI;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.ShowOptions(false);
        StartCoroutine(StartGame(0.5f));
    }
    IEnumerator StartGame(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.CurrentState = GameState.Play;
        UI.ShowKill(true);
        SetInteractable(false);
    }
    public void SetInteractable(bool active)
    {
        panel.interactable = active;
        panel.blocksRaycasts = active;
    }
}
