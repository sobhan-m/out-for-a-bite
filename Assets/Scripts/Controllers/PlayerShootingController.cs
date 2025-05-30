using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float secondsBetweenShots;
    [SerializeField] public int magSize;
    [SerializeField] public float secondsBeforeReload;
    [Range(0f, 1f)] [SerializeField] public float instaReloadMinPercentage;
    [Range(0f, 1f)] [SerializeField] public float instaReloadMaxPercentage;
    private Meter shootingCooldown;
    private Meter mag;
    private Camera cam;
    private InputActionAsset actions;
    private InputAction shootAction;
    private InputAction instaReload;
    private SpriteRenderer spriteRenderer;
    public Meter reloadCooldown { get; private set; }
    private bool canInstaReload = true;


    private void Awake()
    {
        actions = FindObjectOfType<InputActionContainingSystem>().actions;
        shootAction = actions.FindActionMap("Player").FindAction("Shoot");
        shootAction.performed += OnShoot;
        instaReload = actions.FindActionMap("Player").FindAction("InstaReload");
        instaReload.performed += OnInstaReload;
        shootingCooldown = new Meter(0, secondsBetweenShots);
        mag = new Meter(0, magSize, magSize);
        reloadCooldown = new Meter(0, secondsBeforeReload, secondsBeforeReload);
        cam = Camera.main;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        TurnToFaceMouse(FindCharacterMouseDiff().x <= 0);
        CooldownShooting();
        CooldownReload();
    }

    private void Shoot()
    {
        mag.EmptyMeter(1);

        Vector3 diff = FindCharacterMouseDiff();
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
                Reload();
            }
        }
    }

    private void Reload()
    {
        reloadCooldown.FillMeter();
        mag.FillMeter();
        canInstaReload = true;
    }

    private void OnInstaReload(InputAction.CallbackContext context)
    {
        if (!canInstaReload || reloadCooldown.IsFull())
        {
            return;
        }

        bool isBelowMax = reloadCooldown.currentValue <= reloadCooldown.maxValue * instaReloadMaxPercentage;
        bool isAboveMin = reloadCooldown.currentValue >= reloadCooldown.maxValue * instaReloadMinPercentage;

        Debug.Log("reloadCooldown.currentValue = " + reloadCooldown.currentValue);
        Debug.Log("reloadCooldown.maxValue = " + reloadCooldown.maxValue);
        Debug.Log("reloadCooldown.maxValue * instaReloadMaxPercentage = " + reloadCooldown.maxValue * instaReloadMaxPercentage);
        Debug.Log("reloadCooldown.maxValue * instaReloadMinPercentage = " + reloadCooldown.maxValue * instaReloadMinPercentage);
        Debug.Log("isBelowMax = " + isBelowMax);
        Debug.Log("isAboveMin = " + isAboveMin);

        if (isBelowMax && isAboveMin)
        {
            Reload();
        }
        canInstaReload = false;
    }

    private void OnEnable()
    {
        shootAction.Enable();
    }

    private void OnDisable()
    {
        shootAction.Disable();
    }

    private void TurnToFaceMouse(bool isLeft)
    {
        spriteRenderer.flipX = isLeft;
    }

    private Vector3 FindCharacterMouseDiff()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = gameObject.transform.position.z;
        Vector3 currentPosition = gameObject.transform.position;

        Vector3 diff = mousePosition - currentPosition;
        return diff;
    }
}
