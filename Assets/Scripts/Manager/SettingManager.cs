using System;
using System.Collections;
using System.Collections.Generic;
using Tabsil.Sijil;
using UnityEngine;

public class SettingManager : MonoBehaviour, IWantToBeSaved
{
    private const string SFX_STATE = "SFXState";
    private const string MUSIC_STATE = "MusicState";

    public Action<bool> OnSFXStateChanged;
    public Action<bool> OnMusicStateChanged;

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

    private void Start()
    {
        OnSFXStateChanged?.Invoke(SFXState);
        OnMusicStateChanged?.Invoke(MusicState);
    }

    public void SetSFXState()
    {
        SFXState = !SFXState;
        Save();

        OnSFXStateChanged?.Invoke(SFXState);
    }

    public void SetMusicState()
    {
        MusicState = !MusicState;
        Save();

        OnMusicStateChanged?.Invoke(MusicState);
    }

    public void Load()
    {
        if (Sijil.TryLoad(this, MUSIC_STATE, out object musicState))
            MusicState = (bool)musicState;
        else
            MusicState = true;

        if (Sijil.TryLoad(this, SFX_STATE, out object sfxState))
            SFXState = (bool)sfxState;
        else
            SFXState = true;

    }

    public void Save()
    {
        Sijil.Save(this, SFX_STATE, SFXState);
        Sijil.Save(this, MUSIC_STATE, MusicState);
    }
}
