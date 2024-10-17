using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; }

    public bool SFXState { get; private set; }
    public bool MusicState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetSFXState()
    {
        SFXState = !SFXState;
    }

    public void SetMusicState()
    {
        MusicState = !MusicState;
    }
}
