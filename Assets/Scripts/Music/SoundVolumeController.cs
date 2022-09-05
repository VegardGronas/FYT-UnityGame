using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Music,
    Ambient,
    Effects
}

public class SoundVolumeController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioType type;

    private void OnEnable()
    {
        if (SoundManager.Instance)
        {
            SoundManager.Instance._ambientVolumeAction += AdjustAmbient;
            SoundManager.Instance._effectsVolumeAction += AdjustEffects;
        }
    }

    private void OnDisable()
    {
        if (SoundManager.Instance)
        {
            SoundManager.Instance._ambientVolumeAction -= AdjustAmbient;
            SoundManager.Instance._effectsVolumeAction -= AdjustEffects;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (SoundManager.Instance)
        {
            switch (type)
            {
                case AudioType.Music:
                    audioSource.volume = SoundManager.Instance.MusicSource.volume;
                    break;

                case AudioType.Effects:
                    audioSource.volume = SoundManager.Instance.FXVolumeSource.volume; 
                break;
                case AudioType.Ambient:
                    audioSource.volume = SoundManager.Instance.AmbientSource.volume;
                    break;
            }
        }
    }

    private void AdjustEffects(AudioSource source)
    {
        if (type == AudioType.Effects)
        {
            audioSource.volume = source.volume;
            audioSource.mute = source.mute;
        }
    }

    private void AdjustAmbient(AudioSource source)
    {
        if (type == AudioType.Ambient)
        {
            audioSource.volume = source.volume;
            audioSource.mute = source.mute;
        }
    }
}
