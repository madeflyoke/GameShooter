using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    private Recoil _recoil;
    public event Action<int,int> ShootEvent;

    [Header("FireSettings:")]
    public float Damage = 21;
    public float fireRate = 1;
    private float nextTimeToFire = 0f;
    public float fireRange = 15;
    public float force = 155;
    public bool isAutomatic;
    [Space]

    [Header("ParticleEffects:")]
    public ParticleSystem muzzleFlash;
    private Transform bulletSpawn;
    public GameObject ImpactEffect;
    [Space]

    [Header("Sound:")]
    public AudioClip shotSfx;
    public AudioSource _audioSource;
    public AudioClip ReloadSFX;
    [Space]

    [Header("Ammo:")]
    public int maxAmmo=25;
    public int remainAmmo;
    public int currentAmmo=15;
    public float reloadTime = 2f;
    public int numberOfMagazines=3;
    private bool isReloading = false;
    [Space]
    public Camera _cam;
    [Header("Animation:")]
    public Animator _animator;


    void Start()
    {
        _recoil = GetComponent<Recoil>();
        
    }
    private void OnEnable()
    {
        _animator.Play("SwitchingOnAnim");
        isReloading = false;
        _animator.SetBool("Reloading", false);

    }
    void Update()
    {
        if (isReloading)
            return;
        
        if((currentAmmo<=0 || currentAmmo!= maxAmmo / numberOfMagazines && Input.GetKey(KeyCode.R)) && remainAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }
        

        if (isAutomatic)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire&& currentAmmo>0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

    }
    IEnumerator Reload()
    {
        isReloading = true;
        _audioSource.PlayOneShot(ReloadSFX);
        Debug.Log("Reloading...");
        _animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        _animator.SetBool("Reloading", false);

        int amountNeeded = maxAmmo / numberOfMagazines - currentAmmo;
        if (amountNeeded < remainAmmo )
        {
            currentAmmo += amountNeeded;
            remainAmmo -= amountNeeded;
        }
        else if (amountNeeded >= remainAmmo)
        {
            currentAmmo += remainAmmo;
            remainAmmo = 0;
        }
        ShootEvent?.Invoke(currentAmmo, remainAmmo); 
        isReloading = false;
    }
    public void Shoot()
    { 
        _audioSource.PlayOneShot(shotSfx);
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(_cam.transform.position,_cam.transform.forward, out hit, fireRange))
        {
            Debug.Log("HIT " + hit.collider);
        }
        if (hit.rigidbody != null)
            hit.rigidbody.AddForce(-hit.normal * force);
        GameObject tmpHit = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(tmpHit, 1f);

        currentAmmo--;
        ShootEvent?.Invoke(currentAmmo, remainAmmo);
        _recoil.Fire();
        
    }
 
}
