using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontChanger : MonoBehaviour
{
    [SerializeField]
    private List<Button> uiButtons;
    [SerializeField]
    private TMP_FontAsset btnFont;

    private void Start()
    {
        foreach (Button btn in uiButtons)
        {
            btn.GetComponentInChildren<TextMeshProUGUI>().font = btnFont;
        }
    }
}
