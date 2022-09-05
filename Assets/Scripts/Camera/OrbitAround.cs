using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    public GameObject orbitTarget;
    public float rotationSpeed;
    private float newRotate;

    public void OnClickRoate(float dir)
    {
        newRotate += rotationSpeed * dir;
        transform.RotateAround(orbitTarget.transform.position, Vector3.up, newRotate);
    }
}
