using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionButtons : MonoBehaviour
{
    public CharacterScripableObjects character;
    public GameObject prefab;
    public string prefabName;

    [SerializeField]
    private Image CharDisplayImage;
    [SerializeField]
    private TextMeshProUGUI namePlates;
    private MenuButtonEvents menuButtons;

    private void Start()
    {
        if (character != null)
        {
            prefab = character.prefab;
            prefabName = character.name;
        }

        menuButtons = GetComponentInParent<MenuButtonEvents>();
    }

    public void SetUIImage()
    {
        CharDisplayImage.sprite = character.image;
        namePlates.text = character.name;
    }

    public void OnClickAddReadyPlayer(int playerIndex)
    {
        menuButtons.OnClickAddReadyPlayer(playerIndex);
    }

    public void OnClickRemoveReadyPlayer(int playerIndex)
    {
        menuButtons.OnClickRemoveReadyPlayer(playerIndex);
    }
}
