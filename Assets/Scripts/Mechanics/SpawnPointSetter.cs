using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointSetter : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSpawner.Instace.SetCurrentSpawnPoint(spawnPoint);
            Invoke(nameof(x),4); 


        }
    }

    void x() => PlayerSpawner.Instace.SpawnPlayerOnSpawnpoint();

}
