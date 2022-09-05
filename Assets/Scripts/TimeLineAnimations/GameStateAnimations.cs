using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateAnimations : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> UIElements;
    public List<GameObject> playersInScene;


    public void OnGameStateUpdate(GameStates state)
    {
        switch (state)
        {
            case GameStates.Ready:
                UIElements[0].GetComponent<TimelineAnimationManager>().PlayAnimation(state);
                break;
            case GameStates.NewRound:
                UIElements[1].GetComponent<TimelineAnimationManager>().PlayAnimation(state);
                break;
            case GameStates.Gameover:
                UIElements[2].GetComponent<TimelineAnimationManager>().PlayAnimation(state);
                break;
        }
    }
}
