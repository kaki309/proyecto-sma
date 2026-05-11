using UnityEngine;

public class SpawnPointSetter : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSpawner.Instance.SpawnPlayerOnLocation(spawnPoint);
        }
    }
}
