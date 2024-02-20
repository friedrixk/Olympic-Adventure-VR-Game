using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [SerializeField] private List<Snake> snakePrefabs = new List<Snake>();
    [SerializeField] private int snakeCount = 10;

    [SerializeField] private float spawnY = 0.0f;
    [SerializeField] private float spawnXMin = -5.0f;
    [SerializeField] private float spawnXMax = 5.0f;
    [SerializeField] private float spawnZMin = -5.0f;
    [SerializeField] private float spawnZMax = 5.0f;

    [SerializeField] private bool randomizeSnakeSelection = true;

    private int snakeIndex = 0;

    public EventHandler OnAllSnakesKilled = delegate { };

    private List<Snake> snakes = new List<Snake>();

    void Start()
    {
    }

    void Update()
    {
        // Cheat Codes
        if (Input.GetKeyDown(KeyCode.O)) {
            CreateSnakes();
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            ClearSnakes();
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            KillRandomSnake();
        }
    }

    public void KillRandomSnake() {
        if (snakes.Count > 0) {
            int index = UnityEngine.Random.Range(0, snakes.Count);
            Snake snake = snakes[index];
            snake.Kill();
        }
    }

    public void ClearSnakes() {
        foreach (Snake snake in snakes) {
            Destroy(snake.gameObject);
        }
        snakes.Clear();
    }

    public void CreateSnakes() {
        for (int i = 0; i < snakeCount; i++) {
            SpawnSnake();
        }
    }

    public void ResetSnakes() {
        ClearSnakes();
        CreateSnakes();
    }

    public void SpawnSnake() {
        if (randomizeSnakeSelection) {
            snakeIndex = UnityEngine.Random.Range(1, snakePrefabs.Count);
        } else if (++snakeIndex >= snakePrefabs.Count) {
            snakeCount = 0;
        }

        float x = UnityEngine.Random.Range(spawnXMin, spawnXMax);
        float z = UnityEngine.Random.Range(spawnZMin, spawnZMax);
        Vector3 position = new Vector3(x, spawnY, z);
        float yRotation = UnityEngine.Random.Range(0, 360);
        float scale = UnityEngine.Random.Range(0.5f, 1.2f);
        Snake snakePrefab = snakePrefabs[UnityEngine.Random.Range(0, snakePrefabs.Count)];
        Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
        Snake snake = Instantiate(snakePrefab, position, rotation);
        
        snake.OnSnakeKilled += (sender, e) => {
            snakes.Remove(snake);
            if (snakes.Count == 0) {
                OnAllSnakesKilled(this, EventArgs.Empty);
            }
        };
        snakes.Add(snake);
    }
}
