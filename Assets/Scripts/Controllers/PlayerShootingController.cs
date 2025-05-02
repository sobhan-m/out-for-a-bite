using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float secondsBetweenShots;
    private Meter shootingCooldown;
    private Camera cam;
    private InputActionAsset actions;
    private InputAction shootAction;


    private void Awake()
    {
        actions = FindObjectOfType<InputActionContainingSystem>().actions;
        Debug.Log(actions);
        shootAction = actions.FindActionMap("Player").FindAction("Shoot");
    }

    void Start()
    {
        shootingCooldown = new Meter(0, secondsBetweenShots);
        cam = Camera.main;
    }

    void Update()
    {
        shootingCooldown.EmptyMeter(Time.deltaTime);
        if (shootAction.IsPressed() && shootingCooldown.IsEmpty())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = gameObject.transform.position.z;
        Vector3 currentPosition = gameObject.transform.position;

        Vector3 diff = mousePosition - currentPosition;
        float speed = bulletPrefab.GetComponent<Bullet>().speed;
        Vector3 bulletVelocity = diff.normalized * speed;

        GameObject bullet = GameObject.Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;

        shootingCooldown.FillMeter();
    }

    private void OnEnable()
    {
        shootAction.Enable();
    }

    private void OnDisable()
    {
        shootAction.Disable();
    }
}
