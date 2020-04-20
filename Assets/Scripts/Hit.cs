using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public bool IsTouchingBall(Collider2D collider)
    {
        return this.GetComponent<Collider2D>().IsTouching(collider);
    }
}
