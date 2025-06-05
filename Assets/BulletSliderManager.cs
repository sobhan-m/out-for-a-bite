using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSliderManager : MonoBehaviour
{

    [SerializeField] private Image foreground;
    private GunMagazine magazine;
    private Meter reloadCooldown;
    private PlayerShootingController player;

    private int cachedMagazineAmount;

    private void Start()
    {
        // Placing these in Start to ensure they are instantiated already.
        player = FindObjectOfType<PlayerShootingController>();
        magazine = player.magazine;
        cachedMagazineAmount = magazine.currentMagazineBulletCount;
        reloadCooldown = player.reloadCooldown;
    }

    private void Update()
    {
        // Set colour to green during the interval.
        if (player.IsInInstaReloadInterval() && foreground.color == Color.white)
        {
            foreground.color = Color.green;
        }

        // Reset colour to white.
        if (!player.IsInInstaReloadInterval() && foreground.color == Color.green)
        {
            foreground.color = Color.white;
        }

        if (player.IsReloading())
        {
            // We want the image to be full when the cooldown reaches 0.
            foreground.fillAmount = (reloadCooldown.maxValue - reloadCooldown.currentValue) / reloadCooldown.maxValue;
        }
        else if (cachedMagazineAmount != magazine.currentMagazineBulletCount)
        {
            foreground.fillAmount = magazine.currentMagazineBulletCount * 1f / magazine.magazineCapacity;
            cachedMagazineAmount = magazine.currentMagazineBulletCount;
        }
    }
}
