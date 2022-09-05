using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundText : MonoBehaviour
{
    public TextMeshProUGUI roundNumber;
    public TextMeshProUGUI text;
    private int pointsPlayer1;
    private int pointsPlayer2;

    private void OnEnable()
    {
        if(GameManager.Instance.Player1){
            pointsPlayer1 = GameManager.Instance.Player1.Points;
            pointsPlayer2 = GameManager.Instance.Player2.Points;
        }

        int currentRound = GameManager.Instance.CurrentRound;

        if (pointsPlayer1 == 1 && pointsPlayer2 == 1)
        {
            text.text = "Final Round!";
            roundNumber.text = "";
        }
        else
        {
            text.text = "Round ";
            roundNumber.text = currentRound.ToString();
        }

    }
}
