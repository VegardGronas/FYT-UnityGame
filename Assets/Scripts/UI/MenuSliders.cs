using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuSliders : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    public enum GameSettings { Time, Points, Settings}
    public GameSettings gameSetting;
    public Slider slider;

    private void OnEnable()
    {
        switch (gameSetting)
        {
            case GameSettings.Time:
                int time = 60;
                if (text)
                {
                    text.text = time.ToString();
                }
                slider.value = time / 10;
                GameManager.Instance.SetGameTime = time;
                break;
            case GameSettings.Points:
                int points = 2;
                slider.value = points;
                if (text)
                {
                    text.text = points.ToString();
                }
                GameManager.Instance.GamePoints = points;
                break;
        }
    }

    public void SetGameTime(System.Single value)
    {
        float time = value * 10;
        GameManager.Instance.SetGameTime = time;
        text.text = time.ToString();
    }

    public void SetGameScore(System.Single value)
    {
        GameManager.Instance.GamePoints = (int)value;
        text.text = value.ToString();
    }

    public void SetMusicVolume(System.Single value)
    {
        SoundManager.Instance.MusicVolume(value);
    }

    public void SetFXVolume(System.Single value)
    {
        SoundManager.Instance.FXVolume(value);
    }

    public void SetEnvironMentVolume(System.Single value)
    {
        SoundManager.Instance.AmbientVolume(value);
    }
}
