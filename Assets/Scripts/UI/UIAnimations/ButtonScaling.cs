using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ButtonScaling : MonoBehaviour
{
    [SerializeField]
    private Vector2 scaleFactor;
    [SerializeField]
    private Vector2 startScale;
    [SerializeField]
    float speed;

    private void Awake()
    {
        startScale = transform.localScale;
    }

    private void OnEnable()
    {
        LeanTween.scale(gameObject, scaleFactor, speed);
    }

    private void OnDisable()
    {
        gameObject.transform.localScale = startScale;
        LeanTween.cancel(gameObject);
    }
}
