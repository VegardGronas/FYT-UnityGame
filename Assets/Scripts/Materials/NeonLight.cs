using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonLight : MonoBehaviour
{
    [SerializeField]
    private Material neon;
    public float emission;

    private void Update()
    {
        emission = Random.Range(0, 1000);
        if ((int)emission % 5 == 0)
        {
            neon.SetFloat("_Emission", 0);
        }
        else
        {
            neon.SetFloat("_Emission", 30);
        }
    }
}
