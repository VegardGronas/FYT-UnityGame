using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationsButtons : MonoBehaviour
{
    private InputMenu player1Hovering;
    private InputMenu player2Hovering;
    private Image img;

    private void Start()
    {
        img = gameObject.GetComponent<Image>();
    }

    private void OnDisable()
    {
        img.color = Color.white;
    }


    public void ButtonSize(InputMenu im)
    {
        if (im.playerIndex == 0)
        {
            player1Hovering = im;
        }
        else if (im.playerIndex == 1)
        {
            player2Hovering = im;
        }
    }

    public void OnLeaveButtonScale(InputMenu im)
    {
        if (im.playerIndex == 0)
        {
            player1Hovering = null;
        }
        else if (im.playerIndex == 1)
        {
            player2Hovering = null;
        }

        if (player1Hovering == null && player2Hovering == null)
        {
            img.color = Color.white;
        }
        else if (player2Hovering == null)
        {
            ButtonSize(player1Hovering);
        }
        else if (player1Hovering == null)
        {
            ButtonSize(player2Hovering);
        }
    }
}
