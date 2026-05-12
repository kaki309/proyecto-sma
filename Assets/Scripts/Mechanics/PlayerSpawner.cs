using System.ComponentModel;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{   
    [SerializeField] PlayerSpawnLocation[] spawnPoints;
    Transform currentSpawnPoint;
    GameObject player;
    public static PlayerSpawner Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetCurrentSpawnPoint(WorldSpawnPoints spawn)
    {
        foreach (PlayerSpawnLocation spawnPoint in spawnPoints)
        {
            if (spawnPoint.type == spawn)
            {   
                currentSpawnPoint = spawnPoint.location;
                return;
            }
        }
    }

    public void SpawnPlayerOnSpawnpoint(WorldSpawnPoints spawn)
    {
        SetCurrentSpawnPoint(spawn);
        player.transform.position = currentSpawnPoint.position;
    }
    public void SpawnPlayerOnLocation(Transform location)
    {
        player.transform.position = location.position;
    }
}
public enum WorldSpawnPoints
{
    initial,
    outsideLab,
    outsidePtar
}
[System.Serializable]
public class PlayerSpawnLocation
{
    public WorldSpawnPoints type;
    public Transform location;

}