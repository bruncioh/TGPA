using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    protected enum WeaponNames
    {
       None,
       AssultRifle,
       Pistol,
       Shotgun,
       LaserRifle,
       SMG,
    }
    [SerializeField]
    private Recoil mRecoilObject;

    protected Camera fpsCam;

    //Weapon Base Stats
    protected WeaponNames mWeaponName;
    protected float mRange = 100.0f;
    protected float mDamage = 10.0f;
    protected int mAmmo;
    protected int mMagSize;
    protected float mReloadTime;
    protected bool mIsFullAuto = false;

    //Recoil variables
    [SerializeField] protected float mFireTime = 0;
    protected double mFireDelay;
    
    [SerializeField] protected ParticleSystem mMuzzleFlash;
    [SerializeField] protected AudioSource mShootSound;

    private void Start()
    {
        fpsCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        //mRecoilObject = GameObject.Find("CameraRotation/CameraRecoil").GetComponent<Recoil>();
        mWeaponName = WeaponNames.None;
    }

    protected virtual void Update()
    {
        //Fire single Shot if mouse button pressed
       // if (Input.GetButtonDown("Fire1") && mAmmo > 0)
        //{
        //    Shoot();
       //     mRecoilObject.ApplyRecoil();
       //     mAmmo--;
       // }
       // else
       // {
            //rapid fire if mouse button held
           // if (Input.GetMouseButton(0) && mIsFullAuto == true)
           // {
              //  if (mAmmo > 0)
              //  {
               //     mFireTime -= Time.deltaTime;
                //    if (mFireTime < 0)
                 //   {
                 //       Shoot();
                  //      mRecoilObject.ApplyRecoil();
                  //      mFireTime += mFireDelay;
                  //  }
                  //  mAmmo--;
           //     }
           // }
      //  }
        //reset ammo
        if (Time.time > mFireDelay && mAmmo == 0)
        {
            mAmmo = mMagSize;
        }
        //mFireTime = 0;
    }

    protected virtual void Shoot()
    {
        if (mAmmo > 0)
        {
            mShootSound.pitch = Random.Range(0.5f, 0.8f);
            mShootSound.Play();
            mRecoilObject.ApplyRecoil();
            mMuzzleFlash.Play();
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out _, mRange))
            {
                Debug.Log(mAmmo);
                //Debug.Log("Pew");
                //Debug.Log(mDamage);
            }
        }
    }

    public IEnumerator RapitShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(mFireTime);
        }
    }

}
