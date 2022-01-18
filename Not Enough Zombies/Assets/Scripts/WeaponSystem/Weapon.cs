using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
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

    protected Recoil mRecoil;

    protected Camera fpsCam;

    protected float mRange = 100.0f;
    protected float mDamage = 10.0f;
    protected double mFireTime;
    protected double mFireDelay = 10.0f;

    [SerializeField]
    protected WeaponState mState = WeaponState.Default;
    [SerializeField]
    protected ParticleSystem mMuzzleFlash;
    [SerializeField]
    protected AudioSource mShootSound;

    private void Start()
    {
        fpsCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        mFireDelay = 0.01f;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mState = WeaponState.Shoot;
            mFireTime -= Time.deltaTime;
            if (mFireTime < 0)
            {
                Shoot();
                mFireTime += mFireDelay;
            }
        }
        
    }

    protected virtual void Shoot()
    {
        mMuzzleFlash.Play();
        mShootSound.Play();
        mShootSound.pitch = Random.Range(8.0f, 9.0f);
        
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out _, mRange))
        {
            Debug.Log("Pew");
            Debug.Log(mDamage);
        }
    }

}
