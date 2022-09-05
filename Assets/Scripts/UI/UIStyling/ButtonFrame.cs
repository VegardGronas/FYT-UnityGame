using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonFrame : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private Image frameOverlay;

    private void OnEnable()
    {
        frameOverlay.gameObject.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        frameOverlay.gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        frameOverlay.gameObject.SetActive(false);
    }
}
