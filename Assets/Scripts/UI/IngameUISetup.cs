using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameUISetup : MonoBehaviour
{
    [SerializeField]
    private Image[] characterImages;
    [SerializeField]
    private TextMeshProUGUI[] characterNames;

    private InputSetup[] players;

    private void Start()
    {
        players = FindObjectsOfType<InputSetup>();

        foreach (InputSetup player in players)
        {
            characterNames[player.PlayerIndex].text = player.charName;
            characterImages[player.PlayerIndex].sprite = player.charImage;
        }
    }
}
