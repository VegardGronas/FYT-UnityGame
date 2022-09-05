using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;

    private void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
    }
}
