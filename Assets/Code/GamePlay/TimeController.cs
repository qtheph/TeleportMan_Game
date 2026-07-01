using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;
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
    public void SetSlow(float timeScale)
    {
        Time.timeScale = timeScale;
    }
    public void SyncPhysic(float timeScale)
    {
        Time.fixedDeltaTime = 0.02f * timeScale;
    }
    public void ResetNormal()
    {
        Time.timeScale = 1f;
    }
    public void ResetPhysic()
    {
        Time.fixedDeltaTime = 0.02f;
    }
    public IEnumerator SetSlowOnKill(float time)
    {
        SetSlow(0.25f);
        SyncPhysic(0.25f);
        yield return new WaitForSeconds(time);
        ResetNormal();
        ResetPhysic();
    }
}
