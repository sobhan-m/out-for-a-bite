using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunMagazine
{
    private Meter magazine;
    public int magazineCapacity { get { return (int) magazine.maxValue; } }
    public int currentMagazineBulletCount { get { return (int)magazine.currentValue; } }
    public GunMagazine(int magazineCapacity)
    {
        this.magazine = new Meter(0, magazineCapacity, magazineCapacity);
    }

    public GunMagazine(int magazineCapacity, int currentMagazineAmount)
    {
        this.magazine = new Meter(0, magazineCapacity, currentMagazineAmount);
    }

    public bool IsEmpty()
    {
        return this.magazine.IsEmpty();
    }

    public void EmptyShot()
    {
        this.magazine.EmptyMeter(1);
    }

    public void EmptyMagazine()
    {
        this.magazine.EmptyMeter();
    }

    public void Reload()
    {
        this.magazine.FillMeter();
    }

    public void Reload(int amount)
    {
        this.magazine.FillMeter(amount);
    }
}
