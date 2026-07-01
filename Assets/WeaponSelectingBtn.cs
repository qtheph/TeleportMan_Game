using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectingBtn : MonoBehaviour
{
    public Button[] buttons;
    public Transform allWeapon;
    public Transform handWeapon;
    void Start()
    {
        int index = PlayerPrefs.GetInt("UsingWeapon", 0);
        SelectWeapon(index);
    }
    void OnEnable()
    {
        SetupButton();
    }
    private void SetupButton()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() =>
            {
                PlayerPrefs.SetInt("UsingWeapon", index);
                PlayerPrefs.Save();
                SelectWeapon(index);
                AudioManager.Instance.ButtonFX();
            });
        }
    }
    private void SelectWeapon(int index)
    {
        //can't select if not buying
        if (!ShopBtn.Instance.IsUnlocked(index)) return;
        //unactive all weapon
        for (int i = 0; i < allWeapon.childCount; i++)
        {
            Debug.Log("Child" + allWeapon.childCount);
            allWeapon.GetChild(i).gameObject.SetActive(false);
            handWeapon.GetChild(i).gameObject.SetActive(false);
        }
        //active selected weapon
        Transform selectingWeapon = allWeapon.GetChild(index);
        selectingWeapon.gameObject.SetActive(true);

        Transform selectingHandWeapon = handWeapon.GetChild(index);
        selectingHandWeapon.gameObject.SetActive(true);
    }
}
