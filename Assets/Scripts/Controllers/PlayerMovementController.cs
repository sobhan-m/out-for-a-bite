using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] public float speed;
    private Rigidbody2D rigidBody;
    private InputAction moveAction;
    private InputActionAsset actions;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        actions = FindObjectOfType<InputActionContainingSystem>().actions;
        moveAction = actions.FindActionMap("Player").FindAction("Move");
    }

	void Update()
	{
        TurnToFaceMouse();
	}

    private void TurnToFaceMouse()
    {
        spriteRenderer.flipX = InputService.GetDifferenceFromMouse(transform.position).x <= 0;
    }

	void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>() * speed;

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
