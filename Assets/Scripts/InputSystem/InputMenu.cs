using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputMenu : MonoBehaviour
{
    private MultiplayerEventSystem eventSystem;
    private InputSetup inputSetup;
    private GameObject prefab;
    private PlayerInput playerInput;

    public GameObject currentSelected;
    public int playerIndex;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        eventSystem = GetComponent<MultiplayerEventSystem>();
        inputSetup = GetComponent<InputSetup>();

        playerIndex = playerInput.playerIndex;
    }

    public void BackInMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MenuButtonEvents btns = FindObjectOfType<MenuButtonEvents>();
            if (btns != null)
            {
                btns.OnClickBackInMenu();
            }
        }
    }

    public void MoveInMenu(InputAction.CallbackContext context)
    {
        
    }

    public void SelectElement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (eventSystem != null)
            {
                if (eventSystem.currentSelectedGameObject != null)
                {
                    if (eventSystem.currentSelectedGameObject.TryGetComponent(out CharacterSelectionButtons charSelected))
                    {
                        if (charSelected.prefab != null)
                        {
                            prefab = charSelected.prefab;
                            inputSetup.prefab = prefab;
                            inputSetup.charName = charSelected.prefabName;
                            inputSetup.charImage = charSelected.character.image;
                        }
                    }
                    SetButtonToEventSystem lastSelected = eventSystem.currentSelectedGameObject.GetComponentInParent<SetButtonToEventSystem>();
                    if (lastSelected)
                    {
                        lastSelected.selectedButton = eventSystem.currentSelectedGameObject.gameObject;
                    }
                }
            }
        }
    }

    public void L2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SoundManager.Instance.MuteMusic();
        }
    }

    public void R2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SoundManager.Instance.MuteAmbient();
        }
    }

    public void Square(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SoundManager.Instance.MuteFX();
        }
    }
}
