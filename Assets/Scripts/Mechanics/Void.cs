using UnityEngine;

public class Void : MonoBehaviour
{
    [SerializeField] WorldSpawnPoints point;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerSpawner.Instance.SetCurrentSpawnPoint(point);
            collision.GetComponent<PlayerHealth>().TakeDamage();
            PlayerSpawner.Instance.SpawnPlayerOnSpawnpoint(point);
        }
    }
}

