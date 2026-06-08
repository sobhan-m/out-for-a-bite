using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletReserve : MonoBehaviour
{
    [SerializeField] public int garlicCount = 12;

    public bool HasGarlic()
    {
        return garlicCount > 0;
    }

    public void UseGarlic()
    {
        --garlicCount;
    }

    public int TryGetBullets(int bulletCount)
    {
        int bulletsRetrieved = Mathf.Min(bulletCount, this.garlicCount);
        this.garlicCount -= bulletsRetrieved;
        return bulletsRetrieved;
    }
}
