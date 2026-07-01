using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{

    public static Bucket Instance;
    [SerializeField] private int money;
    void Start()
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
        money = PlayerPrefs.GetInt("Money");
    }
    public int GetMoney()
    {
        return money;
    }

    // Update is called once per frame
    public void AddMoney(int amount)
    {
        money += amount;
        SaveMoney();
    }
    public void SendMoney(int amount)
    {
        money -= amount;
        SaveMoney();
    }
    private void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
    }
}
