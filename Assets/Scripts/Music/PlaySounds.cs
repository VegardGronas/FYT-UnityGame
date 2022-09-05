using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    public AudioClip music;

    private void Start()
    {
        SoundManager.Instance.PlayMusic(music);
    }
}
