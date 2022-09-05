using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenAnimations : MonoBehaviour
{
    [SerializeField]
    private Vector2 scaleFactor;
    [SerializeField]
    private float scaleSpeed;

    [SerializeField]
    private bool loopScaling;

    private Vector3 startScale;

    private void OnEnable()
    {
        startScale = transform.localScale;
        if (loopScaling)
        {
            LeanTween.scale(gameObject, scaleFactor, scaleSpeed).setLoopPingPong();
        }
    }

    private void OnDisable()
    {
        transform.localScale = startScale;
        LeanTween.cancel(gameObject);
    }
}
