using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class UiViodeButtons : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoPlayer backgroundVideo;

    public void StartVideo()
    {
        videoPlayer.Play();
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }
}
