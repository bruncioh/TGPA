using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{ 
    private Recoil mRecoilObject;

    protected Camera fpsCam;

    //Weapon Base Stats
    //protected WeaponNames mWeaponName;
    protected float mRange = 100.0f;
    protected float mDamage = 10.0f;
    protected int mAmmo;
    protected int mMagSize;
    protected float mReloadTime;
    [SerializeField] protected double mFireTime = 0;
    [SerializeField] protected double mFireDelay;
    protected RaycastHit mRayHit;

    protected bool mIsFullAuto = false;
    protected bool mIsReloading = false;

    [SerializeField] protected ParticleSystem mMuzzleFlash;
    [SerializeField] protected AudioSource mShootSound;
    [SerializeField] protected AudioSource mReloadSound;
    [SerializeField] protected GameObject mBulletHolePrefab;

    private void Start()
    {
        fpsCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        mRecoilObject = GameObject.Find("CameraRotation/CameraRecoil").GetComponent<Recoil>();
    }

    protected virtual void Update()
    {
        ////Fire single Shot if mouse button pressed
        if (Input.GetButtonDown("Fire1") && mAmmo > 0 && mIsReloading == false)
        {
            Shoot();
            mRecoilObject.ApplyRecoil();
            //mAmmo--;
        }
        else
        {
            //rapid fire if mouse button held
            if (Input.GetMouseButton(0) && mIsFullAuto == true && mIsReloading == false)
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
                    //mAmmo--;
                }
            }
        }
        if (Input.GetButtonDown("Reload"))
        {
            Reload();
            
        }
        //reset ammo
        //if (Time.time > mFireDelay && mAmmo == 0)
        //{
        //    //Shoot();
           
        //}
        
    }
    protected virtual void Shoot()
    {  
        mShootSound.pitch = Random.Range(8.0f, 9.0f);
        mShootSound.Play();
        mMuzzleFlash.Play();
        mAmmo--;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out mRayHit, mRange))
        {
            //if wall hit spawn bullet hole
            if (mRayHit.collider.tag == "Wall")
            {
                //spawn bullet hole
                Instantiate(mBulletHolePrefab, mRayHit.point, Quaternion.LookRotation(mRayHit.normal));
            }
            Debug.Log(mAmmo);
            //Debug.Log("Pew");
            //Debug.Log(mDamage);

        }
    }
    protected virtual void Reload()
    {
        if (mIsReloading == false)
        {
            mReloadSound.Play();
            StartCoroutine(ReloadDelay());
            Debug.Log("Reloading...");

        }
    }

    IEnumerator ReloadDelay()
    {
        mIsReloading = true;
        yield return new WaitForSeconds(mReloadTime);

        if (mAmmo >= 0)
        {
            mAmmo = mMagSize;
        }
        mIsReloading = false;
    }
}
