using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlane : MonoBehaviour
{
    public enum State {
        Bottom,
        ShakingAtBottom,
        MovingUp,
        Top
    }

    [SerializeField]
    private Vector3 startPoint;

    [SerializeField]
    private Vector3 endPoint;

    [SerializeField]

    private float speed = 0.5f;

    [SerializeField]
    private State state = State.Bottom;

    private float startTime = 0f;

    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // Press P to start the sequence
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartSequence();
        }


        switch (state)
        {
            case State.Bottom:
                break;
            case State.ShakingAtBottom:
                UpdateShake();
                break;
            case State.MovingUp:
                transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) * speed);
                break;
            case State.Top:
                break;
            default:
                break;
        }
    }

    void UpdateShake()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.05f, 0.05f));
        transform.position = startPoint + randomOffset;
        if (Time.time - startTime > (0.5f / speed) )
        {
            state = State.MovingUp;
            startTime = Time.time;
        }
    }

    public void StartSequence()
    {
        state = State.ShakingAtBottom;
        startTime = Time.time;
    }

    public void Reset()
    {
        state = State.Bottom;
        transform.position = startPoint;
    }

    public State GetState()
    {
        return state;
    }
}
