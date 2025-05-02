using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] InputActionAsset actions;
    [SerializeField] public float speed;
    private Rigidbody2D rigidBody;
    private InputAction moveAction;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        moveAction = actions.FindActionMap("Player").FindAction("Move");
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>() * speed;
        Debug.Log("movement" + movement);

        rigidBody.velocity = movement;
    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }
    void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
    }
}
