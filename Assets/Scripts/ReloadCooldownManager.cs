using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadCooldownManager : MonoBehaviour
{
    [SerializeField] PlayerShootingController shootingController;
    [SerializeField] Slider slider;

    private void Start()
    {
        slider.minValue = shootingController.reloadCooldown.minValue;
        slider.maxValue = shootingController.reloadCooldown.maxValue;
        slider.value = shootingController.reloadCooldown.currentValue;
    }

    void Update()
    {
        if (shootingController.reloadCooldown.IsFull())
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
        }

        slider.value = shootingController.reloadCooldown.currentValue;
    }

}
