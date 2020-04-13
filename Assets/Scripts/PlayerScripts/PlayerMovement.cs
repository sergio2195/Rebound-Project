using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum MoveType
    {
        Right,
        Left,
        None
    }

    [SerializeField]
    private float speed = 10.0f;
    private Rigidbody2D rb;
    [SerializeField]
    private bool grounded;
    private MoveType moveType;

    public bool touchTwice = false;
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

        if (Input.touchCount > 1)
        {
            touchTwice = true;
            moveType = MoveType.None;
        }
        else
            touchTwice = false;
        Debug.Log(moveType);
    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        if (grounded)
            movePlayer(new Vector2(Input.GetAxis("Horizontal"), 0));
#endif

        if (grounded)
        {
            if (moveType.Equals(MoveType.Left))
                movePlayer(new Vector2(-1.0f, 0f));
            else if (moveType.Equals(MoveType.Right))
                movePlayer(new Vector2(1.0f, 0f));
            else if (moveType.Equals(MoveType.None))
                movePlayer(new Vector2(0f, 0f));
        }
    }

    private void movePlayer(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            grounded = true;

    }
}
