using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] public float speed;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x == 0f && y == 0f)
        {
            return;
        }

        float norm = Mathf.Sqrt(x * x + y * y);
        
        float normalizedX = x / norm;
        float normalizedY = y / norm;

        float newXPosition = rigidBody.position.x + normalizedX * speed * Time.fixedDeltaTime;
        float newYPosition = rigidBody.position.y + normalizedY * speed * Time.fixedDeltaTime;

        rigidBody.MovePosition(new Vector2(newXPosition, newYPosition));
    }
}
