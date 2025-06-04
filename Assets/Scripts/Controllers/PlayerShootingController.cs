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
    private GunMagazine magazine;
    private Camera cam;
    private InputActionAsset actions;
    private InputAction shootAction;
    private InputAction instaReload;
    private SpriteRenderer spriteRenderer;
    public Meter reloadCooldown { get; private set; }
    private bool canInstaReload = true;

    private BulletReserve bulletReserve;

    private void Awake()
    {
        bulletReserve = FindObjectOfType<BulletReserve>();
        actions = FindObjectOfType<InputActionContainingSystem>().actions;
        shootAction = actions.FindActionMap("Player").FindAction("Shoot");
        shootAction.performed += OnShoot;
        instaReload = actions.FindActionMap("Player").FindAction("InstaReload");
        instaReload.performed += OnInstaReload;
        shootingCooldown = new Meter(0, secondsBetweenShots);
        reloadCooldown = new Meter(0, secondsBeforeReload, secondsBeforeReload);
        cam = Camera.main;
        magazine = new GunMagazine(magSize);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        TurnToFaceMouse(IsFacingLeft());
        CooldownShooting();
        CooldownReload();
    }

    private void Shoot()
    {
        magazine.EmptyShot();

        CreateBullet();

        shootingCooldown.FillMeter();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (shootingCooldown.IsEmpty() && !magazine.IsEmpty())
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
        if (magazine.IsEmpty() && bulletReserve.bulletCount != 0)
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

        int bulletsFetched = bulletReserve.TryGetBullets(magSize);
        magazine.Reload(bulletsFetched);

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

    private void TurnBulletFaceMouse(bool isLeft, SpriteRenderer bulletSprite)
    {
        bulletSprite.flipY = isLeft;
    }

    private float FindBulletAngle()
    {
        Vector2 playerMouseVector = FindCharacterMouseDiff();
        float angleRadian = Mathf.Atan2(playerMouseVector.y, playerMouseVector.x);
        return angleRadian * Mathf.Rad2Deg;
    }

    private bool IsFacingLeft()
    {
        return FindCharacterMouseDiff().x <= 0;
    }

    private void CreateBullet()
    {
        Vector3 diff = FindCharacterMouseDiff();
        float speed = bulletPrefab.GetComponent<Bullet>().speed;
        Vector3 bulletVelocity = diff.normalized * speed;

        GameObject bullet = GameObject.Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, FindBulletAngle()));
        TurnBulletFaceMouse(IsFacingLeft(), bullet.GetComponent<SpriteRenderer>());
        bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
    }
}
