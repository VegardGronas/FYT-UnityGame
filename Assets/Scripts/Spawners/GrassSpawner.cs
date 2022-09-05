using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    [SerializeField]
    private int grassAmount;
    [SerializeField]
    private GameObject grassPrefab;
    [SerializeField]
    private Vector3 min;
    [SerializeField]
    private Vector3 max;

    private void Start()
    {
        for (int i = 0; i < grassAmount; i++)
        {
            Vector3 spawnPoint = randomVector(min, max);
            Instantiate(grassPrefab, spawnPoint, Quaternion.identity);
        }
    }

    private Vector3 randomVector(Vector3 min, Vector3 max)
    {
        Vector3 respons = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
        return respons;
    }
}
