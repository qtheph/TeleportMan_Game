using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioFX;
    [SerializeField] AudioClip enemyDie;
    [SerializeField] AudioClip winGame;
    [SerializeField] AudioClip loseGame;
    [SerializeField] AudioClip boomFX;
    [SerializeField] AudioClip buttonFX;
    [SerializeField] AudioClip doubleKill;
    [SerializeField] AudioClip trippleKill;
    [SerializeField] AudioClip masterKill;
    public void Start()
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
    public void EnemyDie()
    {
        PlayOneShotFX(enemyDie);
    }
    public void DoubleKill()
    {
        PlayOneShot(doubleKill);
    }
    public void TrippleKill()
    {
        PlayOneShot(trippleKill);
    }
    public void MasterKill()
    {
        PlayOneShot(masterKill);
    }
    public void Win()
    {
        PlayOneShot(winGame);
    }
    public void Lose()
    {
        PlayOneShot(loseGame);
    }
    public void BoomFX()
    {
        PlayOneShotFX(boomFX);
    }
    public void ButtonFX()
    {
        PlayOneShot(buttonFX);
    }
    public void Mute(bool active)
    {
        audioSource.mute = active;
    }
    private void PlayOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    private void PlayOneShotFX(AudioClip audioClip)
    {
        audioFX.PlayOneShot(audioClip);
    }
}
