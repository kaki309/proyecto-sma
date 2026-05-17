using Onigwrap;
using UnityEngine;

public class SpawnPointSetter : MonoBehaviour
{
    [Header("SpawnPoint (Use only one)")]
    public Transform spawnPoint;
    public WorldSpawnPoints worldSpawn;
    [SerializeField] GameObject boardSprite;
    void OnEnable()
    {
        PlayerSpawner.OnSpawnPointChanged += turnOffBoardSprite;
    }
    void OnDisable()
    {
        PlayerSpawner.OnSpawnPointChanged -= turnOffBoardSprite;
    }
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        boardSprite.SetActive(false);
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
            boardSprite.SetActive(true);
        }
    }
    void turnOffBoardSprite(Transform currentSpawnPoint)
    {
        if (spawnPoint != null)
        {
            if (currentSpawnPoint == spawnPoint) return;
        }
        else
        {
            foreach (PlayerSpawnLocation spawn in PlayerSpawner.Instance.spawnPoints)
            {
                if (worldSpawn == spawn.type)
                {
                    if (currentSpawnPoint == spawn.location) return;
                }
            }
        }

        boardSprite.SetActive(false);
    }
}
