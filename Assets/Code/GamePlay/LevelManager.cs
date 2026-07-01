using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public event Action OnWin;
    public event Action NextOrResetLevel;
    [SerializeField] CameraController cameraController;
    [SerializeField] UIManager UIManager;
    [SerializeField] GameObject[] mapPrefab;
    [SerializeField] private PlayerBase player;
    [SerializeField] private PlayerWeaponBase weapon;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ShopBtn shopBtn;
    private GameObject currLevel;
    private Coroutine stopCoroutine;
    private int currLevelIndex;
    private int gameLevel;
    private int iconIndex;
    public int enemyCount;
    private int coinGained;
    private int enemyKilled;
    private int enemySoundKillCount;

    void Start()
    {
        gameLevel = PlayerPrefs.GetInt("GameLevel", 1);
        currLevelIndex = PlayerPrefs.GetInt("Level");
        UIManager.ShowLevelText(currLevelIndex);
        TimeController.Instance.ResetNormal();
        LoadLevel(currLevelIndex);
        player.InitPlayer(spawnPoint);
        weapon.InitWeapon(spawnPoint);

    }
    void OnEnable()
    {
        EnemyBase.OnEnemyDead += EnemyDead;
        shopBtn.InOpen += OpenShop;
        shopBtn.InClose += CloseShop;

    }
    void OnDisable()
    {
        EnemyBase.OnEnemyDead -= EnemyDead;
    }
    void LoadLevel(int index)
    {
        if (currLevel != null)
        {
            Destroy(currLevel);
            UIManager.DestroyIcon();
            iconIndex = 0;
        }
        currLevel = Instantiate(mapPrefab[index], transform);
        enemyCount = currLevel.GetComponentsInChildren<EnemyBase>().Length;
        enemyKilled = enemyCount;
        enemySoundKillCount = 0;
        coinGained = enemyCount * 100;
        LevelType levelType = currLevel.GetComponent<LevelType>();

        UIManager.SpawnIcon(enemyCount);
        player.InitPlayer(spawnPoint);
        weapon.InitWeapon(spawnPoint);
        UIManager.ShowLevelText(gameLevel);
        NextOrResetLevel?.Invoke();
        //Debug.Log(levelType.type);
        if (levelType != null && levelType.type == Type.Boss)
        {
            EnemyBase bossPos = currLevel.GetComponentInChildren<EnemyBase>();
            Debug.Log("Boss Pos" + bossPos);
            if (bossPos == null) return;
            cameraController.ViewBoss(bossPos.transform);
        }
    }
    public void Next()
    {
        LoadLevel(currLevelIndex);
        AudioManager.Instance.ButtonFX();
    }
    public void TryAgain()
    {
        LoadLevel(currLevelIndex);
        AudioManager.Instance.ButtonFX();
    }

    private void EnemyDead()
    {
        enemyCount--;
        enemySoundKillCount++;
        PlayKillSound();

        UIManager.MarkEnemyDead(iconIndex);
        iconIndex++;

        if (enemyCount == 0)
        {
            UIManager.ShowLevelWin(gameLevel);
            UIManager.ShowEnemyKilled(enemyKilled);
            UIManager.ShowCoinGained(coinGained);

            Bucket.Instance.AddMoney(coinGained);

            currLevelIndex++;
            if (currLevelIndex >= mapPrefab.Length) currLevelIndex = 0;
            gameLevel++;
            SaveLevel();
            OnWin?.Invoke();
        }
    }
    private void PlayKillSound()
    {
        if (enemySoundKillCount == 2)
        {
            UIManager.ShowKillCount(true);
            UIManager.ChangeSpriteDoubleKill();
            AudioManager.Instance.DoubleKill();
        }
        else if (enemySoundKillCount == 3)
        {
            UIManager.ShowKillCount(true);
            UIManager.ChangeSpriteTrippleKill();
            AudioManager.Instance.TrippleKill();
        }
        else if (enemySoundKillCount > 3)
        {
            UIManager.ShowKillCount(true);
            UIManager.ChangeSpriteMasterKill();
            AudioManager.Instance.MasterKill();
        }
        // Stop old coroutine
        if (stopCoroutine != null) StopCoroutine(stopCoroutine);
        // Reset coroutine
        stopCoroutine = StartCoroutine(ResetKillCountAfterTime());
    }

    IEnumerator ResetKillCountAfterTime()
    {
        yield return new WaitForSeconds(3f);
        UIManager.ShowKillCount(false);
        enemySoundKillCount = 0;
    }
    private void OpenShop()
    {
        LoadLevel(currLevelIndex);
    }
    private void CloseShop()
    {
        LoadLevel(currLevelIndex);
    }
    private void SaveLevel()
    {
        PlayerPrefs.SetInt("GameLevel", gameLevel);
        PlayerPrefs.SetInt("Level", currLevelIndex);
        PlayerPrefs.Save();
    }

}
