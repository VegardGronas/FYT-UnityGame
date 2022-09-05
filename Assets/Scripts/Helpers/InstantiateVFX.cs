using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstantiateVFX : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject vfx;
    [SerializeField]
    private float destroyTimer;

    public void SpawnVFX()
    {
        GameObject go = Instantiate(vfx, spawnPoint.transform.position, Quaternion.identity);
        Destroy(go, destroyTimer);
    }
}
