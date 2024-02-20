using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField]
    private MovingPlane movingPlane;

    private List<Snake> snakes = new List<Snake>();

    void Awake()
    {
       StartLevel();
       FetchSnakes();
    }

    void Start()
    {
       StartLevel();
        FetchSnakes();
    }

    void FetchSnakes() {
        snakes.Clear();
        Snake[] snakeArray = FindObjectsOfType<Snake>();
        foreach (Snake snake in snakeArray) {
            snakes.Add(snake);
        }
    }

    void Update()
    {
        // Cheat Codes
        if (Input.GetKeyDown(KeyCode.S)) {
            StartLevel();
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            foreach (Snake snake in snakes) {
                snake.Kill();
            }
        }

        if (movingPlane.GetState() == MovingPlane.State.Bottom) {
            bool allDead = true;
            foreach (Snake snake in snakes) {
                if (snake.IsAlive()) {
                    allDead = false;
                    break;
                }
            }
            if (allDead) {
                movingPlane.StartSequence();
            }
        }
    }

    public void StartLevel()
    {
        movingPlane.Reset();
        foreach (Snake snake in snakes) {
            snake.Revive();
        }
    }
}
