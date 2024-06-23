using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] Obstacles;

    public float ObstacleFallSpeed;
    public float SpawnTimer;

    private float originalSpawnTimer = 0;
    private float originalObstacleFallSpeed = 0;

    private bool _spawn;

    private float _timer;
    
    void Start()
    {
        _timer = 0;
        originalSpawnTimer = SpawnTimer;
        originalObstacleFallSpeed = ObstacleFallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawn)
        {
            if (Timer())
            {
                InitSpawn();
            }
        }
    }

    public void SetSpawner(bool spawn)
    {
        _spawn = spawn;
    }

    bool Timer()
    {
        bool result = false;

        _timer += Time.deltaTime;

        if (_timer >= SpawnTimer)
        {
            _timer = 0;
            result = true;
        }

        return result;
    }

    void InitSpawn()
    {
        int randomObstacle = Random.Range(0, Obstacles.Length);
        int randomLocation = Random.Range(0, SpawnPoints.Length);
        var clone = Instantiate(Obstacles[randomObstacle], SpawnPoints[randomLocation].position, Quaternion.identity);
        clone.GetComponent<Obstacle>().SetObstacleSpeed(ObstacleFallSpeed);
    }

    public void ResetSpawner()
    {
        SpawnTimer = originalSpawnTimer;
        ObstacleFallSpeed = originalObstacleFallSpeed;
    }
}
