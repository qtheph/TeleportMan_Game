using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    //public static UIManager Instance;
    [SerializeField] private GameObject icon;
    [SerializeField] private Transform spawnIconPoint;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject killUI;
    [SerializeField] private Image KillCountBanner;
    [SerializeField] private Sprite doubleKill;
    [SerializeField] private Sprite trippleKill;
    [SerializeField] private Sprite masterKill;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textLevelWin;
    [SerializeField] private TextMeshProUGUI textEnemyKilled;
    [SerializeField] private TextMeshProUGUI textCoinGained;
    [SerializeField] private Slider slider;

    public void ShowWin(bool active) => winUI.SetActive(active);
    public void ShowLose(bool active) => loseUI.SetActive(active);
    public void ShowKill(bool active) => killUI.SetActive(active);
    public void ShowKillCount(bool active) => KillCountBanner.gameObject.SetActive(active);

    public void ChangeSpriteDoubleKill() => KillCountBanner.sprite = doubleKill;

    public void ChangeSpriteTrippleKill() => KillCountBanner.sprite = trippleKill;

    public void ChangeSpriteMasterKill() => KillCountBanner.sprite = masterKill;


    public void SpawnIcon(int numberIcon)
    {
        for (int i = 0; i < numberIcon; i++)
        {
            Instantiate(icon, spawnIconPoint);
        }
    }
    public void DestroyIcon()
    {
        foreach (Transform child in spawnIconPoint)
        {
            Destroy(child.gameObject);
        }
    }
    public void MarkEnemyDead(int index)
    {
        Transform child = spawnIconPoint.GetChild(index);
        child.GetChild(0).gameObject.SetActive(true);
    }
    public void SetSliderLoading(float value)
    {
        slider.value = value;
    }
    public void ShowLevelText(int levelIndex)
    {
        textLevel.text = "Level " + levelIndex;
    }
    public void ShowLevelWin(int levelIndex)
    {
        textLevelWin.text = "LEVEL " + levelIndex + " COMPLETED";
    }
    public void ShowEnemyKilled(int enemyCount)
    {
        textEnemyKilled.text = "x" + enemyCount;
    }
    public void ShowCoinGained(int coinCount)
    {
        textCoinGained.text = "x" + coinCount;
    }
}
