using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class PlayerMovement : MonoBehaviour
{
    private enum MoveType
    {
        Right,
        Left,
        None
    }

    [SerializeField] private GameObject ball, hitArea;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private bool grounded;
    [SerializeField] private bool touchTwice = false;

    private Rigidbody2D rb;
    private MoveType moveType;
    private Vector2 angleHit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width / 2)
            {
                moveType = MoveType.Left;
            }
            if (touch.position.x > Screen.width / 2)
            {
                moveType = MoveType.Right;
            }
        }
        if(Input.touchCount == 0)
            moveType = MoveType.None;

        if (Input.touchCount > 1 || Input.GetKeyDown(KeyCode.Space))
        {
            touchTwice = true;
            moveType = MoveType.None;
            if(this.GetComponentInChildren<Hit>().IsTouchingBall(ball.GetComponent<Collider2D>()))
                ball.GetComponent<BallMovement>().moveBall(hitBall(ball.transform.position));
        }
        else
            touchTwice = false;
    }

    private void FixedUpdate()
    {
    #if UNITY_EDITOR
        if (grounded)
            movePlayer(new Vector2(Input.GetAxis("Horizontal"), 0));
    #endif

        if (grounded)
        {
            if (moveType.Equals(MoveType.None))
                movePlayer(new Vector2(0f, 0f));
            else if (moveType.Equals(MoveType.Left))
                movePlayer(new Vector2(-1.0f, 0f));
            else if (moveType.Equals(MoveType.Right))
                movePlayer(new Vector2(1.0f, 0f));
        }
    }

    private void movePlayer(Vector2 direction)
    {
        //rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        rb.AddForce(direction * speed);
    }

    public Vector2 hitBall(Vector2 ballPosition)
    {
        // Aqui para calcular el ángulo en radianes, restamos la posición 'y' de la bola respecto a la base del área de golpe, 
        // dividimos entre la altura de la misma y multiplicamos por 90º = π/2
        float angle = Mathf.Rad2Deg * (ballPosition.y - (hitArea.transform.position.y - hitArea.transform.localScale.y)) / 
            hitArea.transform.lossyScale.y * (Mathf.PI / 2);
        Debug.Log("Angulo de salida: " + angle);
        angleHit = new Vector2 (Mathf.Cos(angle), Mathf.Sin(angle));
        return angleHit;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            grounded = true;

    }
}
