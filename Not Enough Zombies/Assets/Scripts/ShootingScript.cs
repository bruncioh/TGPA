using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public float bulletDamage = 10.0f;
    public float range = 100.0f;

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;

    public AudioSource shootSound;

    private void Update()
    {
        

    }

    public void Shoot()
    {
        RaycastHit raycastHit;
        muzzleFlash.Play();
        shootSound.Play();
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out raycastHit, range))
        {
            Debug.Log("Pew");
        }

    }
    
}
