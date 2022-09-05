using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVariables : MonoBehaviour
{
    [SerializeField]
    private GameObject startObject;
    [SerializeField]
    private GameObject startPage;
    public bool playerJoined;

    private void OnEnable()
    {
        ResetCanvas();
    }

    public void ResetCanvas()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        if (playerJoined)
        {
            if (!startPage.activeInHierarchy)
            {
                startPage.SetActive(true);
            }
        }
        else
        {
            if (!startObject.activeInHierarchy)
            {
                startObject.SetActive(true);
            }
        }
    }
}
