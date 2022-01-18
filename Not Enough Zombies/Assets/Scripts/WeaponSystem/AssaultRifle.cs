using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{
    // Start is called before the first frame update
    void Awake()
    {
        mRange = 100.0f;
        mDamage = 50.0f;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Shoot()
    {
        base.Shoot();
    }

}
