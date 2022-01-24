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
    private Recoil mRecoilObject;

    protected Camera fpsCam;

    //Weapon Base Stats
    protected float mRange = 100.0f;
    protected float mDamage = 10.0f;
    protected int mAmmo;
    protected int mMagSize;
    //
    [SerializeField] protected double mFireTime = 0;
    protected double mFireDelay;
    protected float mReloadTime;

    [SerializeField] protected WeaponState mState = WeaponState.Default;
    [SerializeField] protected ParticleSystem mMuzzleFlash;
    [SerializeField] protected AudioSource mShootSound;

    private void Start()
    {
        fpsCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        mRecoilObject = GameObject.Find("CameraRotation/CameraRecoil").GetComponent<Recoil>();
    }

    protected virtual void Update()
    {
        //Fire single Shot if mouse button pressed
        if (Input.GetButtonDown("Fire1") && mAmmo > 0)
        {
            Shoot();
            mRecoilObject.ApplyRecoil();
            mAmmo--;
        }
        else
        {
            //rapid fire if mouse button held
            if (Input.GetMouseButton(0))
            {
                if (mAmmo > 0)
                {
                    mFireTime -= Time.deltaTime;
                    if (mFireTime < 0)
                    {
                        Shoot();
                        mRecoilObject.ApplyRecoil();
                        mFireTime += mFireDelay;
                    }
                    mAmmo--;
                }
            }
        }
        //reset ammo
        if (Time.time > mFireDelay && mAmmo == 0)
        {
            mAmmo = mMagSize;
        }
       
    }
    protected virtual void Shoot()
    {
       
        mShootSound.pitch = Random.Range(8.0f, 9.0f);
        mShootSound.Play();
        mMuzzleFlash.Play();
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out _, mRange))
        {
            Debug.Log(mAmmo);
            //Debug.Log("Pew");
            //Debug.Log(mDamage);
        }
    }

}
