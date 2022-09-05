using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctionsPlayerInput : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> buttonOverlay;
    [SerializeField]
    private List<GameObject> videoPlayers;

    public void SetButtonOverlay(int playerIndex)
    {
        buttonOverlay[playerIndex].SetActive(true);
    }

    public void RemoveButtonOverlay(int playerIndex)
    {
        buttonOverlay[playerIndex].SetActive(false);
    }

    public void EnableNewDisableOldMenu(GameObject obj)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        if (obj.activeInHierarchy)
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }
    }

    public void EnableDisableVideoPlayer(GameObject toEnable)
    {
        foreach (GameObject go in videoPlayers)
        {
            go.SetActive(false);
        }
        toEnable.SetActive(true);
    }

    public void EnableUiElement(GameObject go)
    {
        go.SetActive(go);
    }

    public void DisableUiElemet(GameObject go)
    {
        go.SetActive(false);
    }
}
