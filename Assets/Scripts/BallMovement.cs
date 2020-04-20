using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    [SerializeField] private Vector2 initialDirection;
    [SerializeField] private float initialPower, power;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(initialDirection * initialPower);
    }

    public void moveBall(Vector2 direction)
    {
        rb.AddForce(direction * power);
        Debug.Log("Posicion bola: " + GetComponent<Transform>().position);
        Debug.Log("Vector de golpe: " + direction);
    }
}
