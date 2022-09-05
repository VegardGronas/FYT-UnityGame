using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> UIAnimations;
    [SerializeField]
    private InGameUI inGameUI;
    private float maxGameTime;

    public GameStates setGameState;

    private List<GameObject> anims;

    private void OnEnable()
    {
        GameManager.Instance._gameState += ChangeState;
    }

    private void OnDisable()
    {
        GameManager.Instance._gameState -= ChangeState;
    }

    private void Start()
    {
        GameManager.Instance.OnUpdateGameState(setGameState);
        maxGameTime = GameManager.Instance.SetGameTime;

        anims = new List<GameObject>();
        anims = UIAnimations;
    }

    public void ChangeState(GameStates newState)
    {
        if (inGameUI)
        {
            if (newState == GameStates.Play)
            {
                inGameUI.StartCountDown = true;
            }
            else
            {
                inGameUI.StartCountDown = false;
            }
        }

        switch (newState)
        {
            case GameStates.IntroPlayer1:
                if (inGameUI)
                {
                    inGameUI.timer.text = GameManager.Instance.SetGameTime.ToString("F0");
                }
                break;
            case GameStates.NewRound:
                inGameUI.GameTime = maxGameTime;
                break;
            case GameStates.Ready:
                inGameUI.GameTime = maxGameTime;
                anims[0].GetComponent<TimelineAnimationManager>().PlayAnimation(newState);
                break;
            case GameStates.RoundOver:
                anims[1].GetComponent<TimelineAnimationManager>().PlayAnimation(newState);
                break;
            case GameStates.Gameover:
                inGameUI.GameOver(GameManager.Instance.Winner.characterInfo.name);
                anims[2].GetComponent<TimelineAnimationManager>().PlayAnimation(newState);
                break;
            case GameStates.Pause:
                inGameUI.PauseMenu(true);
                break;
            case GameStates.Resume:
                inGameUI.PauseMenu(false);
                break;
        }
    }
}
