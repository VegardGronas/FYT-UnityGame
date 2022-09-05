using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultValuesSliders : MonoBehaviour
{
    public List<Slider> sliders;
    private enum SliderMode { Volume, GameSettings };

    [SerializeField]
    private SliderMode currentMode;

    private void Awake()
    {
        switch (currentMode)
        {
            case SliderMode.Volume:
                sliders[0].value = SoundManager.Instance.MusicSource.volume;
                sliders[1].value = SoundManager.Instance.FXVolumeSource.volume;
                sliders[2].value = SoundManager.Instance.AmbientSource.volume;
                break;
            case SliderMode.GameSettings:
                if (PlayerPrefs.GetInt("GameTime") <= 0)
                {
                    sliders[0].value = 60;
                }
                else
                {
                    sliders[0].value = PlayerPrefs.GetInt("GameTime");
                }
                if (PlayerPrefs.GetInt("GamePoints") <= 0)
                {
                    sliders[1].value = 3;
                }
                else
                {
                    sliders[1].value = PlayerPrefs.GetInt("GamePoints");
                }
                break;
        }
    }
}
