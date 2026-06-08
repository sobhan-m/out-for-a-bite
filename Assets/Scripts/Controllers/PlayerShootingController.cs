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

    // Bullet Count
    [InspectorLabel("Bullet Count")]
    [SerializeField] public int magSize;
    public GunMagazine magazine { get; private set; }
    private BulletReserve bulletReserve;

    // Input
    private InputActionAsset actions;
    private InputAction shootAction;
    private InputAction reload;

    // General
    private Camera cam;

    // Unity Events
    public UnityEvent shootEvent;

    // ====================================
    //  EVENTS
    // ====================================

    private void Awake()
    {
        cam = Camera.main;

        actions = FindObjectOfType<InputActionContainingSystem>().actions;
        shootAction = actions.FindActionMap("Player").FindAction("Shoot");
        reload = actions.FindActionMap("Player").FindAction("Reload");
        shootAction.performed += OnShoot;
        reload.performed += OnReload;

        bulletReserve = FindObjectOfType<BulletReserve>();
        magazine = new GunMagazine(magSize);

        shootingCooldown = new Meter(0, secondsBetweenShots);
    }

    void Update()
    {
        CooldownShooting();
    }

    private void OnEnable()
    {
        shootAction.Enable();
        reload.Enable();
    }

    private void OnDisable()
    {
        shootAction.Disable();
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

    // ====================================
    // PUBLIC METHODS
    // ====================================

    public bool IsReloading()
    {
        return magazine.IsEmpty() && bulletReserve.bulletCount != 0;
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

    private void TurnBulletFaceMouse(bool isLeft, SpriteRenderer bulletSprite)
    {
        bulletSprite.flipY = isLeft;
    }

    private bool IsFacingLeft()
    {
        return InputService.GetDifferenceFromMouse(transform.position, cam).x <= 0;
    }

    private void CreateBullet()
    {
        Vector3 diff = InputService.GetDifferenceFromMouse(transform.position, cam);
        float speed = bulletPrefab.GetComponent<Bullet>().speed;
        Vector3 bulletVelocity = diff.normalized * speed;

        GameObject bullet = GameObject.Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, InputService.FindDegreeFromMouse(transform.position, cam)));
        TurnBulletFaceMouse(IsFacingLeft(), bullet.GetComponent<SpriteRenderer>());
        bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
    }
}
