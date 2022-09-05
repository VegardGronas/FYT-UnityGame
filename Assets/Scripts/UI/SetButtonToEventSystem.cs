using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonToEventSystem : MonoBehaviour
{
    private InputSetup[] inputSetups;
    public GameObject player1Button;
    public GameObject player2Button;

    public GameObject selectedButton;

    private void OnEnable()
    {
        SetButtonToPlayers();
    }

    private void SetButtonToPlayers()
    {
        inputSetups = FindObjectsOfType<InputSetup>();

        foreach (InputSetup setup in inputSetups)
        {
            if (setup.gameObject != null)
            {
                if (player1Button == null && player2Button == null)
                {
                    if (selectedButton && selectedButton.activeInHierarchy)
                    {
                        setup.SetSelectedGameObject(selectedButton);
                    }
                    else
                    {
                        setup.SetSelectedGameObject(gameObject.GetComponentInChildren<Button>().gameObject);
                    }
                }
                else if (setup.PlayerIndex == 0)
                {
                    setup.SetSelectedGameObject(player1Button);
                }
                else if (setup.PlayerIndex == 1)
                {
                    setup.SetSelectedGameObject(player2Button);
                }
            }
        }
    }

    public void SetButtonToJoinedPlayer(InputSetup setup)
    {
        setup.firstSelectedGameObject = player2Button;
    }
}
