using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletReserve : MonoBehaviour
{
    [SerializeField] public int bulletCount = 12;

    public bool CanBlock()
    {
        return bulletCount > 0;
    }

    public void Block()
    {
        --bulletCount;
    }

    public int TryGetBullets(int bulletCount)
    {
        int bulletsRetrieved = Mathf.Min(bulletCount, this.bulletCount);
        this.bulletCount -= bulletsRetrieved;
        return bulletsRetrieved;
    }
}
