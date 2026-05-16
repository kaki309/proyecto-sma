using UnityEngine;

public class Void : MonoBehaviour
{
    [SerializeField] WorldSpawnPoints pointToSpawn;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage();
            PlayerSpawner.Instance.SpawnPlayerOnWorldSpawnpoint(pointToSpawn);
        }
    }
}

