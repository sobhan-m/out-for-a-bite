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
    [SerializeField] private AudioClip shotSoundClip;
    private Meter shootingCooldown;

    // Bullets
    private GarlicReserve garlicReserve;

    // Input
    private InputActionAsset actions;
    private InputAction shootAction;

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

        shootingCooldown = new Meter(0, secondsBetweenShots);
    }

    private void Start()
    {
        garlicReserve = FindObjectOfType<GarlicReserve>();
    }

    void Update()
    {
        CooldownShooting();
    }

    private void OnEnable()
    {
        shootAction.performed += OnShoot;
        shootAction.Enable();
    }

    private void OnDisable()
    {
        shootAction.performed -= OnShoot;
        shootAction.Disable();
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

        if (shootingCooldown.IsEmpty() && garlicReserve.HasGarlic())
        {
            Shoot();
        }
    }

    // ====================================
    // PUBLIC METHODS
    // ====================================

    // ====================================
    // PRIVATE METHODS
    // ====================================

    private void Shoot()
    {
        shootEvent.Invoke();

        garlicReserve.UseGarlic();

        CreateBullet();
        AudioSource.PlayClipAtPoint(shotSoundClip, cam.transform.position);

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
