using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    Transform currentSpawnPoint;
    GameObject player;
    public static PlayerSpawner Instace { get; private set; }
    void Awake()
    {
        if (Instace != null && Instace != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instace = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetCurrentSpawnPoint(Transform spawn)
    {
        currentSpawnPoint = spawn;
    }


    public void SpawnPlayerOnSpawnpoint()
    {
        player.transform.position = currentSpawnPoint.position;
    }
}
