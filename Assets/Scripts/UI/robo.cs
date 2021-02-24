using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class robo : MonoBehaviour
{
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveDirection = Vector2.Reflect(moveDirection, collision.contacts[0].normal);
    }

    private void Update()
    {
        rb.velocity = moveDirection * speed * Time.deltaTime;
        if (moveDirection.x > 0)
            sr.flipX = true;
        else
            sr.flipX = false;
    }
}
