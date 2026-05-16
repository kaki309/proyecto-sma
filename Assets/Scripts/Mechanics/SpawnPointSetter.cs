using UnityEngine;

public class SpawnPointSetter : MonoBehaviour
{
    [Header("SpawnPoint (Use only one)")]
    public Transform spawnPoint;
    public WorldSpawnPoints worldSpawn;

    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (spawnPoint != null)
            {
                PlayerSpawner.Instance.SetCurrentSpawnPoint(spawnPoint);
            }
            else
            {
                if (worldSpawn == WorldSpawnPoints.none)
                {
                    Debug.Log("Posición de spawn puesta en 'none' ");
                    return;
                }
                PlayerSpawner.Instance.SetCurrentSpawnPoint(worldSpawn);
            }
        }
    }
}
