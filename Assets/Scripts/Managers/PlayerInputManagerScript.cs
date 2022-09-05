using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManagerScript : MonoBehaviour
{
    private PlayerInputManager pm;
    public GameObject startPage;
    public GameObject charSelection;

    private void Start()
    {
        pm = GetComponent<PlayerInputManager>();
        pm.onPlayerJoined += OnPlayerJoined;
    }

    private void OnPlayerJoined(PlayerInput input)
    {
        InputSetup setup = input.GetComponent<InputSetup>();

        if (input.playerIndex == 0)
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas)
            {
                canvas.GetComponent<ButtonFunctionsPlayerInput>().EnableNewDisableOldMenu(startPage);
                canvas.GetComponent<CanvasVariables>().playerJoined = true;
            }
            setup.SetSelectedGameObjectOnJoined();
        }
        else if (input.playerIndex == 1)
        {
            if (charSelection.activeInHierarchy)
            {
                setup.SetSelectedGameObjectCharacterSelection();
            }
            else
            {
                setup.SetSelectedGameObjectOnJoined();
            }
        }
    }
}
