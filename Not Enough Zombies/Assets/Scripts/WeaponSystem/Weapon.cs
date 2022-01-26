using UnityEngine;

public abstract class Weapon : MonoBehaviour
{ 
    private Recoil mRecoilObject;

    protected Camera fpsCam;

    //Weapon Base Stats
    //protected WeaponNames mWeaponName;
    protected bool mIsFullAuto = false;

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
            if (Input.GetMouseButton(0) && mIsFullAuto == true)
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
        //mFireTime = 0;
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
