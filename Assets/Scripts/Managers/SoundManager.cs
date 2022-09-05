using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource music;
    public AudioSource soundFX;
    public AudioSource ambient;

    public Action<AudioSource> _ambientVolumeAction;
    public Action<AudioSource> _effectsVolumeAction;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.GetFloat("GameMusic") > 0)
        {
            MusicSource.volume = PlayerPrefs.GetFloat("GameMusic");
        }
        if (PlayerPrefs.GetFloat("AmbientVolume") > 0)
        {
            AmbientSource.volume = PlayerPrefs.GetFloat("AmbientVolume");
        }
        if (PlayerPrefs.GetFloat("FXVolume") > 0)
        {
            FXVolumeSource.volume = PlayerPrefs.GetFloat("FXVolume");
        }
    }

    public AudioSource FXVolumeSource { get { return soundFX; } private set { soundFX = value; _effectsVolumeAction.Invoke(FXVolumeSource); } }
    public AudioSource AmbientSource { get { return ambient; } private set { ambient = value; _ambientVolumeAction.Invoke(AmbientSource); } }
    public AudioSource MusicSource { get { return music; } private set { music = value;} }

    public void MusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("GameMusic", volume);
        MusicSource.volume = volume;
    }

    public void AmbientVolume(float volume)
    {
        PlayerPrefs.SetFloat("AmbientVolume", volume);
        AmbientSource.volume = volume;
    }

    public void FXVolume(float volume)
    {
        PlayerPrefs.SetFloat("FXVolume", volume);
        FXVolumeSource.volume = volume;
    }

    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public void PlaySoundFX(AudioClip clip)
    {
        FXVolumeSource.clip = clip;
        FXVolumeSource.Play();
    }

    public void MuteMusic()
    {
        if (MusicSource.mute)
        {
            MusicSource.mute = false;
        }
        else
        {
            MusicSource.mute = true;
        }
    }

    public void MuteAmbient()
    {
        if (AmbientSource.mute)
        {
            AmbientSource.mute = false;
        }
        else
        {
            AmbientSource.mute = true;
        }
    }

    public void MuteFX()
    {
        if (FXVolumeSource.mute)
        {
            FXVolumeSource.mute = false;
        }
        else
        {
            FXVolumeSource.mute = true;
        }
    }
}
