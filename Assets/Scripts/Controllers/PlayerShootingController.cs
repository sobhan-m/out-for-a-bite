using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    // Shooting
    [InspectorLabel("Shooting")]
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float secondsBetweenShots;
    private Meter shootingCooldown;

    // Reload
    [InspectorLabel("Reload")]
    [SerializeField] public float secondsBeforeReload;
    [Range(0f, 1f)][SerializeField] public float instaReloadMinPercentage;
    [Range(0f, 1f)][SerializeField] public float instaReloadMaxPercentage;
    public Meter reloadCooldown { get; private set; }
    private bool canInstaReload = true;

    // Bullet Count
    [InspectorLabel("Bullet Count")]
    [SerializeField] public int magSize;
    public GunMagazine magazine { get; private set; }
    private BulletReserve bulletReserve;

    // Input
    private InputActionAsset actions;
    private InputAction shootAction;
    private InputAction instaReload;
    private InputAction reload;

    // General
    private SpriteRenderer spriteRenderer;

    // Unity Events
    public UnityEvent shootEvent;

    // ====================================
    //  EVENTS
    // ====================================

    private void Awake()
    {
        actions = FindObjectOfType<InputActionContainingSystem>().actions;
        shootAction = actions.FindActionMap("Player").FindAction("Shoot");
        instaReload = actions.FindActionMap("Player").FindAction("InstaReload");
        reload = actions.FindActionMap("Player").FindAction("Reload");
        shootAction.performed += OnShoot;
        instaReload.performed += OnInstaReload;
        reload.performed += OnReload;

        bulletReserve = FindObjectOfType<BulletReserve>();
        magazine = new GunMagazine(magSize);

        shootingCooldown = new Meter(0, secondsBetweenShots);
        reloadCooldown = new Meter(0, secondsBeforeReload, secondsBeforeReload);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // TurnToFaceMouse(IsFacingLeft());
        CooldownShooting();
        CooldownReload();
    }

    private void OnEnable()
    {
        shootAction.Enable();
        instaReload.Enable();
        reload.Enable();
    }

    private void OnDisable()
    {
        shootAction.Disable();
        instaReload.Disable();
        reload.Disable();
    }

    // ====================================
    // INPUT HANDLERS
    // ====================================

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (PauseController.isPaused)
		{
            return;
		}

        if (shootingCooldown.IsEmpty() && !magazine.IsEmpty())
        {
            Shoot();
        }
    }

    private void OnReload(InputAction.CallbackContext context)
    {
        if (PauseController.isPaused)
		{
            return;
		}


        magazine.EmptyMagazine();
    }

    private void OnInstaReload(InputAction.CallbackContext context)
    {
        if (PauseController.isPaused)
		{
            return;
		}

        if (!canInstaReload || reloadCooldown.IsFull())
        {
            return;
        }

        if (IsInInstaReloadInterval())
        {
            Reload();
        }
        canInstaReload = false;
    }

    // ====================================
    // PUBLIC METHODS
    // ====================================

    public bool IsReloading()
    {
        return magazine.IsEmpty() && bulletReserve.bulletCount != 0;
    }

    public bool IsInInstaReloadInterval()
    {
        bool isBelowMax = reloadCooldown.currentValue <= reloadCooldown.maxValue * instaReloadMaxPercentage;
        bool isAboveMin = reloadCooldown.currentValue >= reloadCooldown.maxValue * instaReloadMinPercentage;
        return isBelowMax && isAboveMin;
    }

    // ====================================
    // PRIVATE METHODS
    // ====================================

    private void Shoot()
    {
        shootEvent.Invoke();

        magazine.EmptyShot();

        CreateBullet();

        shootingCooldown.FillMeter();
    }
    
    private void CooldownShooting()
    {
        shootingCooldown.EmptyMeter(Time.deltaTime);
    }

    private void CooldownReload()
    {
        if (IsReloading())
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

    private void TurnBulletFaceMouse(bool isLeft, SpriteRenderer bulletSprite)
    {
        bulletSprite.flipY = isLeft;
    }

    private bool IsFacingLeft()
    {
        return InputService.GetDifferenceFromMouse(transform.position).x <= 0;
    }

    private void CreateBullet()
    {
        Vector3 diff = InputService.GetDifferenceFromMouse(transform.position);
        float speed = bulletPrefab.GetComponent<Bullet>().speed;
        Vector3 bulletVelocity = diff.normalized * speed;

        GameObject bullet = GameObject.Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, InputService.FindDegreeFromMouse(transform.position)));
        TurnBulletFaceMouse(IsFacingLeft(), bullet.GetComponent<SpriteRenderer>());
        bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
    }
}
