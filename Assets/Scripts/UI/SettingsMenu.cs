using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> volumeNumber;
    [SerializeField]
    private List<Slider> sliders;
    private float music;
    private float fx;
    private float environment;

    private void Start()
    {
        music = Mathf.Round( SoundManager.Instance.MusicSource.volume * 100);
        fx = Mathf.Round(SoundManager.Instance.FXVolumeSource.volume * 100);
        environment = Mathf.Round(SoundManager.Instance.AmbientSource.volume * 100);
        volumeNumber[0].text = music.ToString();
        volumeNumber[1].text = fx.ToString();
        volumeNumber[2].text = environment.ToString();

        sliders[0].value = SoundManager.Instance.MusicSource.volume;
        sliders[1].value = SoundManager.Instance.FXVolumeSource.volume;
        sliders[2].value = SoundManager.Instance.AmbientSource.volume;
    }

    public void OnVolumeChange()
    {
        music = Mathf.Round(SoundManager.Instance.MusicSource.volume * 100);
        sliders[1].value = SoundManager.Instance.FXVolumeSource.volume;
        environment = Mathf.Round(SoundManager.Instance.AmbientSource.volume * 100);
        volumeNumber[0].text = music.ToString();
        volumeNumber[1].text = fx.ToString();
        volumeNumber[2].text = environment.ToString();
    }
}
