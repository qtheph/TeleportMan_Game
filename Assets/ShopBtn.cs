using System;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class ShopBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public static ShopBtn Instance;
    public event Action InOpen;
    public event Action InClose;
    public Transform[] items;
    public int indexItem;
    [SerializeField] GameObject shopPanel;
    [SerializeField] private int unlockPrice;
    [SerializeField] private bool[] unlockedWeapon;
    [SerializeField] UIManager UI;
    void Start()
    {
        Instance = this;
        Init();
        LoadWeaponUI();
    }

    private void Init()
    {
        unlockedWeapon = new bool[items.Length];
        unlockedWeapon[0] = true;
        for (int i = 0; i < items.Length; i++)
        {
            unlockedWeapon[i] = PlayerPrefs.GetInt("Weapon_" + i, i == 0 ? 1 : 0) == 1;
        }
    }
    public void Open()
    {
        shopPanel.SetActive(true);
        UI.ShowKill(false);
        InOpen?.Invoke();
        AudioManager.Instance.ButtonFX();
    }
    public void Close()
    {
        shopPanel.SetActive(false);
        UI.ShowKill(true);
        InClose?.Invoke();
        AudioManager.Instance.ButtonFX();
    }
    public void UnLock()
    {
        if (Bucket.Instance.GetMoney() < unlockPrice || IsUnlocked(indexItem)) return;
        UnlockWeapon(indexItem);
        SaveWeaponUnlocked(indexItem);
        Bucket.Instance.SendMoney(unlockPrice);
        ShowWeaponUIChange();
    }
    private void ShowWeaponUIChange()
    {
        //change color
        Image img = items[indexItem].GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 1f);
        //change background
        items[indexItem].GetChild(0).gameObject.SetActive(false);
        items[indexItem].GetChild(1).gameObject.SetActive(true);
    }
    private void LoadWeaponUI()
    {
        for (int i = 1; i < items.Length; i++)
        {
            if (unlockedWeapon[i])
            {
                ShowWeaponUIChange();
            }
        }
    }
    private void UnlockWeapon(int index)
    {
        unlockedWeapon[index] = true;
    }
    public bool IsUnlocked(int index)
    {
        return unlockedWeapon[index];
    }
    private void SaveWeaponUnlocked(int index)
    {
        PlayerPrefs.SetInt("Weapon_" + index, 1);
        PlayerPrefs.Save();
    }
}
