using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAttacks : MonoBehaviour
{
    public Vector2 velocity;

    private void Update()
    {
        transform.position += Time.deltaTime * new Vector3(velocity.x, velocity.y, 0);
    }
}
