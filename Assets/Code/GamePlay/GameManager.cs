using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState
{
    Play,
    Pause,
    GameOver
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private PanelClick panelClick;
    public UIManager UI;
    [SerializeField] GameObject UILoading;
    [SerializeField] GameObject UIGame;
    [SerializeField] GameObject mini_Options;
    public GameState CurrentState;
    public static GameManager Instance;
    public LevelManager levelManager;
    public PlayerBase player;
    public int enemyCount;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        StartCoroutine(LoadingScreen());
    }

    void OnEnable()
    {
        player.OnPlayerDead += HandleGameLose;
        levelManager.OnWin += HandleGameWin;
        levelManager.NextOrResetLevel += NextOrResetGame;
    }
    void OnDisable()
    {
        player.OnPlayerDead -= HandleGameLose;
        levelManager.OnWin -= HandleGameWin;
        levelManager.NextOrResetLevel -= NextOrResetGame;
    }
    private void HandleGameLose()
    {
        CurrentState = GameState.GameOver;
        StartCoroutine(DelayShowLose());
    }
    private void HandleGameWin()
    {
        CurrentState = GameState.GameOver;
        StartCoroutine(DelayShowWin());
    }
    private void NextOrResetGame()
    {
        TimeController.Instance.ResetNormal();
        UI.ShowWin(false);
        UI.ShowLose(false);
        StartCoroutine(LoadingScreen());
    }
    public void ShowOptions(bool active)
    {
        mini_Options.SetActive(active);
    }
    IEnumerator LoadingScreen()
    {
        CurrentState = GameState.Pause;

        panelClick.SetInteractable(false);

        UILoading.SetActive(true);
        UIGame.SetActive(false);

        ShowOptions(false);

        UI.SetSliderLoading(0f);
        yield return new WaitForSeconds(0.5f);

        UI.SetSliderLoading(0.3f);
        yield return new WaitForSeconds(2f);

        UI.SetSliderLoading(1f);
        yield return new WaitForSeconds(0.5f);

        UILoading.SetActive(false);
        UIGame.SetActive(true);

        ShowOptions(true);
        UI.ShowKill(false);

        panelClick.SetInteractable(true);
    }
    IEnumerator DelayShowWin()
    {
        yield return new WaitForSeconds(2f);
        UI.ShowKill(false);
        UI.ShowLose(false);
        UI.ShowWin(true);
        AudioManager.Instance.Win();
    }
    IEnumerator DelayShowLose()
    {
        yield return new WaitForSeconds(2f);
        UI.ShowKill(false);
        UI.ShowWin(false);
        UI.ShowLose(true);
        AudioManager.Instance.Lose();
    }
}
