using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float secondsBetweenShots;
    private Meter shootingCooldown;
    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        shootingCooldown = new Meter(0, secondsBetweenShots);
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        shootingCooldown.EmptyMeter(Time.deltaTime);
        if (ShouldShoot())
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

    private bool ShouldShoot()
    {
        float x = Input.GetAxis("Fire1");
        return x > Mathf.Epsilon && shootingCooldown.IsEmpty();
    }
}
