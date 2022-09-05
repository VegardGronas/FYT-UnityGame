using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineAnimationManager : MonoBehaviour
{
    private PlayableDirector director;

    public GameStates newState;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += OnAnimationStopped;
        director.played += OnAnimationPlayed;
    }

    private void OnDisable()
    {
        director.stopped -= OnAnimationStopped;
        director.played -= OnAnimationPlayed;
    }

    private void OnAnimationPlayed(PlayableDirector playableDirector)
    {

    }

    private void OnAnimationStopped(PlayableDirector playableDirector)
    {
        if (newState == GameStates.Ready)
        {
            GameManager.Instance.OnUpdateGameState(GameStates.Play);
        }
        else if (newState == GameStates.RoundOver)
        {
            GameManager.Instance.OnUpdateGameState(GameStates.Ready);
        }
    }

    public void PlayAnimation(GameStates state)
    {
        newState = state;
        director.Play();
    }
}
