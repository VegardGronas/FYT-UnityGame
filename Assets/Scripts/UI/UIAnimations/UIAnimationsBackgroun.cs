using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationsBackgroun : MonoBehaviour
{
    public Vector2 moveTo;
    public float speed;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnEnable()
    {
        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, moveTo, speed).setEaseInBack();
    }

    private void OnDisable()
    {
        LeanTween.move(gameObject, startPos, speed);
    }
}
