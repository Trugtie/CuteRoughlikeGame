using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header(" Elements ")]
    private AudioSource _audioSrc;

    private void Awake()
    {
        _audioSrc = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Weapon.OnAnyAttackSound += OnAnyAttackSoundCallback;
        RangeWeapon.OnAnyShootSound += OnAnyShootSoundCallback;
        SettingManager.Instance.OnSFXStateChanged += OnSFXStateChangedCallback;
    }

    private void OnDestroy()
    {
        Weapon.OnAnyAttackSound -= OnAnyAttackSoundCallback;
        RangeWeapon.OnAnyShootSound -= OnAnyShootSoundCallback;
        SettingManager.Instance.OnSFXStateChanged -= OnSFXStateChangedCallback;
    }

    private void OnSFXStateChangedCallback(bool isSFXOn)
    {
        _audioSrc.mute = !isSFXOn;
    }

    private void OnAnyShootSoundCallback(WeaponDataSO weaponDataSO)
    {
        PlayAudio(weaponDataSO.AttackSound, transform.position, 1f);
    }

    private void OnAnyAttackSoundCallback(WeaponDataSO weaponDataSO)
    {
        PlayAudio(weaponDataSO.AttackSound, transform.position, 1f);
    }

    private void PlayAudio(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        _audioSrc.clip = audioClip;
        _audioSrc.pitch = Random.Range(.95f, 1.05f);
        _audioSrc.volume = volume;

        _audioSrc.Play();
    }
}
