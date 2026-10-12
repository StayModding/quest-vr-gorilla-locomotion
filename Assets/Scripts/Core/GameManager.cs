using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform dragonSpawnPoint;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Dragon dragon;

    private readonly List<GameObject> players = new List<GameObject>();
    private readonly List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterPlayer(GameObject player)
    {
        if (!players.Contains(player))
            players.Add(player);
    }

    public void RegisterEnemy(GameObject enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public GameObject GetClosestPlayer(Vector3 origin)
    {
        GameObject closest = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            if (player == null)
                continue;

            float distance = Vector3.Distance(player.transform.position, origin);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                closest = player;
            }
        }

        return closest;
    }

    public void StartWave(int waveIndex)
    {
        if (enemySpawner != null)
        {
            enemySpawner.SpawnWave(waveIndex);
        }
    }

    public Dragon Dragon => dragon;
    public Transform PlayerSpawnPoint => playerSpawnPoint;
    public Transform DragonSpawnPoint => dragonSpawnPoint;
}
