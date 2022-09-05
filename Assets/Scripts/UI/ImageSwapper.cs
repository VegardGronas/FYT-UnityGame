using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class ImageSwapper : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> backgroundImages;
    [SerializeField]
    private Image replaceImage;

    public PlayableDirector director;

    public void Selected()
    {
        int random = backgroundImages.Count - 1;
        replaceImage.sprite = backgroundImages[random];
        if (director)
        {
            director.Play();
        }
    }

    public void Deselect()
    {
        if (director)
        {
            director.Stop();
        }
    }
}
