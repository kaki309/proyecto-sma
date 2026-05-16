using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] PlayerSpawnLocation[] spawnPoints;
    Transform currentSpawnPoint;
    GameObject player;
    Rigidbody2D playerRb;
    public static PlayerSpawner Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        SetCurrentSpawnPoint(WorldSpawnPoints.initial);
    }
    // SET SPAWNPOINT BASED ON WORLD LOCATIONS
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
    // SET SPAWNPOINT BASED ON TRANSFORM
    public void SetCurrentSpawnPoint(Transform location)
    {
        currentSpawnPoint = location;
    }
    #region Spawners API
    public void SpawnPlayerOnCurrentSpawnPoint()
    {
        ChangePlayerPos(currentSpawnPoint);
    }
    public void SpawnPlayerOnWorldSpawnpoint(WorldSpawnPoints spawn)
    {
        foreach (PlayerSpawnLocation spawnPoint in spawnPoints)
        {
            if (spawnPoint.type == spawn)
            {
                ChangePlayerPos(spawnPoint.location);
                return;
            }
        }
    }
    public void SpawnPlayerOnLocation(Transform location)
    {
        ChangePlayerPos(location);
    }
    #endregion

    void ChangePlayerPos(Transform location)
    {
        player.transform.position = location.position;
        playerRb.velocity = new Vector2(0, 0);
    }
}
public enum WorldSpawnPoints
{
    none,
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