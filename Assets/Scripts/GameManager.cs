using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Border[] Borders;
    public int Points;

    [SerializeField]
    private int _score = 0;
    private int _highScore = 0;
    private int _scoreDiffIncrease = 0;


    private Spawner _spawner;
    private UIController _uiController;

    public static GameManager instance;

    void Start()
    {
        instance = this;

        _spawner = FindObjectOfType<Spawner>();
        _uiController = FindObjectOfType<UIController>();

        InitGame();
    }
 
    void InitGame()
    {
        _uiController.ShowScreen(0);
        Borders[(int)Random.Range(0, 1)].SetTarget();
    }

    private bool startDiff = false;
    public void Score(int points)
    {
        _score+= points;
        _uiController.UpdateScore(_score);
        _scoreDiffIncrease += points;

        if (_scoreDiffIncrease >= 100 && !startDiff)
        {
            startDiff = true;
            _scoreDiffIncrease = 0;
            DifficultyIncrease(0.25f, 25);
        }

        if (_scoreDiffIncrease >= 50 && startDiff)
        {
            _scoreDiffIncrease = 0;
            DifficultyIncrease(0.15f, 5);
        }
    }

    public void StopGame(Player player)
    {
        // Stop player movement
        player.DontMove();

        // Stop spawner
        _spawner.SetSpawner(false);

        // Stop block movement
        foreach (var obstacle in FindObjectsOfType<Obstacle>())
        {
           obstacle.SetObstacleSpeed(0);
        }

        // Show High Score
        if (_score > _highScore)
        {
            _highScore = _score;
        }

        _uiController.ShowScreen(2);
        _uiController.ShowHighScore(_highScore, _score);
    }   
    
    public void ClearGame(Player player)
    {
        // destroy all obstacle
        foreach(var obstacle in FindObjectsOfType<Obstacle>())
        {
            Destroy(obstacle.gameObject);
        }

        player.transform.position = Vector3.zero;
        _score = 0;
        _uiController.UpdateScore(_score);

        _spawner.ResetSpawner();

        startDiff = false;
        _scoreDiffIncrease = 0;

        InitGame();
    }    

    public void DifficultyIncrease(float timeIncrease = 0, float obstacleSpeedIncrease = 0)
    {
        _spawner.SpawnTimer -= timeIncrease;
        _spawner.ObstacleFallSpeed += obstacleSpeedIncrease;
    }

}
