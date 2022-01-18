using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected enum WeaponState
    {
        Default,
        Shoot,
        ShootHold,
        EmptyMag,
        Reloading,
        Aim,
        AimShoot,
    }

    public Camera fpsCam;

    protected float mRange = 100.0f;
    protected float mDamage = 10.0f;
    protected float mSpeed = 1.0f;

    protected WeaponState mState;

    // Start is called before the first frame update
    void Start()
    {
        mState = WeaponState.Default;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
