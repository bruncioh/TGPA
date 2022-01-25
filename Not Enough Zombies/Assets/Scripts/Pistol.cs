using UnityEngine;

public class Pistol : Weapon
{
    void Awake()
    {
        mWeaponName = WeaponNames.Pistol;
        mRange = 100.0f;
        mDamage = 50.0f;
        mMagSize = 10;
        mFireDelay = 0.125;
        mAmmo = mMagSize;
        mIsFullAuto = false;
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
