using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon 
{
    private Vector2 mSpread;
    // Start is called before the first frame update
    void Awake()
    {
        mFireDelay = 0.225;
        mMagSize = 8;
        mIsFullAuto = false;
        mWeaponName = WeaponNames.Shotgun;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Shoot()
    {
        base.Shoot();
    }
}
