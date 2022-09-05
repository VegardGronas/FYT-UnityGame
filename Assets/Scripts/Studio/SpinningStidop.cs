using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningStidop : MonoBehaviour
{
    public bool NextCharacter;
    [SerializeField]
    private float rotSpeed;
    [SerializeField]
    private float rotationAngle;
    public float rot;

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y + rotationAngle, 0), Time.deltaTime * rotSpeed);
    }

    public void RotatePodium(int dir)
    {
        rotationAngle += rot * dir;
    }
}
