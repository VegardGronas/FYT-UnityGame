using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject previousMenu;
    [SerializeField]
    private Image mapImage;
    [SerializeField]
    private List<Sprite> mapSprites;
    public int ReadyPlayer1 { get; private set; }
    public int ReadyPlayer2 { get; private set; }
    [SerializeField]
    private GameObject mapMenu;

    [SerializeField]
    private bool enableMainMenu;

    public bool player1Ready;
    public bool player2Ready;

    private void OnDisable()
    {
        player1Ready = false;
        player2Ready = false;
    }

    public void OnClickMenuChange(GameObject nextMenu)
    {
        nextMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnClickBackInMenu()
    {
        if (previousMenu != null)
        {
            if (enableMainMenu)
            {
                gameObject.GetComponentInParent<CanvasVariables>().ResetCanvas();
            }
            else
            {
                OnClickMenuChange(previousMenu);
            }
        }
    }

    public void OnClickSetGameMap(int map)
    {
        GameManager.Instance.Map = map;
    }

    public void PlayAudio(AudioClip clip)
    {
        SoundManager.Instance.PlaySoundFX(clip);
    }

    public void OnClickAddReadyPlayer(int playerIndex)
    {
        if (playerIndex == 0)
        {
            player1Ready = true;
        }
        else
        {
            player2Ready = true;
        }

        if (player1Ready && player2Ready)
        {
            OnClickMenuChange(mapMenu);
        }
    }

    public void OnClickRemoveReadyPlayer(int playerIndex)
    {
        if (playerIndex == 0)
        {
            if (ReadyPlayer1 > 0)
            {
                ReadyPlayer1--;
            }
        }
        else if (playerIndex == 1)
        {
            if (ReadyPlayer2 > 0)
            {
                ReadyPlayer2--;
            }
        }
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }
}
