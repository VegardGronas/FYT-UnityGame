using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlectionButtonFrame : MonoBehaviour
{
    public GameObject selected;

    private void OnEnable()
    {
        if (selected)
        {
            selected.SetActive(false);
        }
    }

    public void SelectCharacter(GameObject newSlected)
    {
        if (selected)
        {
            selected.SetActive(false);
        }
        newSlected.SetActive(true);
        selected = newSlected;
    }
}
