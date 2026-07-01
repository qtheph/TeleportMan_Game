using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBtn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject btnOn;
    [SerializeField] GameObject btnOff;
    [SerializeField] GameObject menu;
    void Start()
    {
        int index = PlayerPrefs.GetInt("Sound", 1);
        Debug.Log("index now" + index);
        if (index == 0)
        {
            On();
        }
        else
        {
            Off();
        }
    }
    public void OpenMenu()
    {
        menu.SetActive(true);
        AudioManager.Instance.ButtonFX();
    }
    public void CloseMenu()
    {
        menu.SetActive(false);
        AudioManager.Instance.ButtonFX();
    }
    public void Off()
    {
        btnOn.SetActive(true);
        btnOff.SetActive(false);
        TurnOnSound();
        AudioManager.Instance.ButtonFX();
    }
    public void On()
    {
        btnOff.SetActive(true);
        btnOn.SetActive(false);
        TurnOffSound();
    }
    private void TurnOnSound()
    {
        AudioManager.Instance.Mute(false);
        PlayerPrefs.SetInt("Sound", 1);
        PlayerPrefs.Save();
    }
    private void TurnOffSound()
    {
        AudioManager.Instance.Mute(true);
        PlayerPrefs.SetInt("Sound", 0);
        PlayerPrefs.Save();
    }
}
