using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    public EventHandler OnSnakeKilled = delegate { };
    public enum Type {
        Basic,
        Green,
        Red,
        Blue,
        Yellow
    }

    public static Dictionary<Type, List<Ball.Type>> ballsThatHitSnake = new Dictionary<Type, List<Ball.Type>> {
        { Type.Basic , new List<Ball.Type> { Ball.Type.Basic , Ball.Type.Green , Ball.Type.Red , Ball.Type.Blue , Ball.Type.Yellow } },
        { Type.Green , new List<Ball.Type> { Ball.Type.Basic , Ball.Type.Green , Ball.Type.Red , Ball.Type.Blue , Ball.Type.Yellow } },
        { Type.Red , new List<Ball.Type> { Ball.Type.Red } },
        { Type.Blue , new List<Ball.Type> { Ball.Type.Blue } },
        { Type.Yellow , new List<Ball.Type> { Ball.Type.Yellow } }
    };

    [SerializeField]
    private Type type = Type.Basic;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.gameObject.name + " OnTriggerEnter " + other.gameObject.name);
        Ball ball = other.GetComponent<Ball>();
        if (ball != null) {
            if (ballsThatHitSnake[type].Contains(ball.getType())) {
                Kill();
                return;
            }
        }
        if (other.gameObject.tag == "Lightning" && (type == Type.Yellow || type == Type.Basic)) {
            Kill();
        }
    }

    public void Kill() {
        Debug.Log("DissolveSelf");
        OnSnakeKilled(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }

    public void Revive() {
        gameObject.SetActive(true);
    }

    public bool IsAlive() {
        return gameObject.activeSelf;
    }
}
