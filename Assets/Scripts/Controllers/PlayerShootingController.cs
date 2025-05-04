using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float secondsBetweenShots;
    [SerializeField] public int magSize;
    [SerializeField] public float secondsBeforeReload;
    private Meter shootingCooldown;
    private Meter mag;
    private Camera cam;
    private InputActionAsset actions;
    private InputAction shootAction;
    private Meter reloadCooldown;


    private void Awake()
    {
        actions = FindObjectOfType<InputActionContainingSystem>().actions;
        shootAction = actions.FindActionMap("Player").FindAction("Shoot");
        shootAction.performed += OnShoot;
    }

    void Start()
    {
        shootingCooldown = new Meter(0, secondsBetweenShots);
        mag = new Meter(0, magSize, magSize);
        reloadCooldown = new Meter(0, secondsBeforeReload);
        cam = Camera.main;
    }

    void Update()
    {
        CooldownShooting();
        CooldownReload();
    }

    private void Shoot()
    {
        mag.EmptyMeter(1);

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

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (shootingCooldown.IsEmpty() && !mag.IsEmpty())
        {
            Shoot();
        }
    }

    private void CooldownShooting()
    {
        shootingCooldown.EmptyMeter(Time.deltaTime);
    }

    private void CooldownReload()
    {
        if (mag.IsEmpty())
        {
            reloadCooldown.EmptyMeter(Time.deltaTime);
            if (reloadCooldown.IsEmpty())
            {
                reloadCooldown.FillMeter();
                mag.FillMeter();
            }
        }
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
