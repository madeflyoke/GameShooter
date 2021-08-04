using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{

    [SerializeField]
    private WeaponData weaponData;
    public WeaponData weaponDataClone { get; private set; }

    private WeaponAnimation weaponAnimation;
    private AudioSource audioSource;
    private Camera bulletSpawner;

    private void Awake()
    {
        weaponDataClone  =  Instantiate(weaponData);

        weaponAnimation = GetComponent<WeaponAnimation>();
        audioSource = GetComponent<AudioSource>();
        bulletSpawner = GetComponentInParent<Camera>();
        
    }
    void Start()
    {
        
    }
    private void OnEnable()
    {
        weaponAnimation.animator.Play("SwitchingOnAnim");
        weaponDataClone.IsReloading = false;
        weaponAnimation.animator.SetBool("Reloading", false);

    }
    void Update()
    {
        if (weaponDataClone.IsReloading)
            return;
        if ((weaponDataClone.CurrentAmmo <= 0 || weaponDataClone.CurrentAmmo != weaponDataClone.MagazineAmmo
            && Input.GetKey(KeyCode.R)) && weaponDataClone.RemainAmmo> 0)
        {
            StartCoroutine(Reload());
            return;
        }
                 
        if (weaponDataClone.IsAutomatic)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= weaponDataClone.NextTimeToFire && weaponDataClone.CurrentAmmo > 0)
            {
                weaponDataClone.NextTimeToFire = Time.time + 1f / weaponDataClone.FireRate;
                Shoot();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= weaponDataClone.NextTimeToFire && weaponDataClone.CurrentAmmo > 0)
        {
            weaponDataClone.NextTimeToFire = Time.time + 1f / weaponDataClone.FireRate;
            Shoot();
        }
    }
    IEnumerator Reload()
    {
        weaponDataClone.IsReloading = true;
        audioSource.PlayOneShot(weaponDataClone.ReloadSfx);
        Debug.Log("Reloading...");
        weaponAnimation.animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(weaponDataClone.ReloadTime);
        weaponAnimation.animator.SetBool("Reloading", false);

        int amountNeeded = weaponDataClone.MagazineAmmo - weaponDataClone.CurrentAmmo;
        if (amountNeeded < weaponDataClone.RemainAmmo)
        {
            weaponDataClone.CurrentAmmo += amountNeeded;
            weaponDataClone.RemainAmmo -= amountNeeded;
        }
        else if (amountNeeded >= weaponDataClone.RemainAmmo)
        {
            weaponDataClone.CurrentAmmo += weaponDataClone.RemainAmmo;
            weaponDataClone.RemainAmmo = 0;
        }
        weaponDataClone.IsReloading = false;
        EventManager.CallOnAmmoChanged(weaponDataClone.CurrentAmmo, weaponDataClone.RemainAmmo);
    }
    public void Shoot()
    {
        audioSource.PlayOneShot(weaponDataClone.ShotSfx);
        weaponDataClone.MuzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(bulletSpawner.transform.position, bulletSpawner.transform.forward, out hit, weaponDataClone.FireRange))
        {
            Debug.Log("HIT " + hit.collider);
        }
        if (hit.rigidbody != null)
            hit.rigidbody.AddForce(-hit.normal * weaponDataClone.Force);
        GameObject tmpHit = Instantiate(weaponDataClone.ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(tmpHit, 1f);
        weaponDataClone.CurrentAmmo--;
        EventManager.CallOnAmmoChanged(weaponDataClone.CurrentAmmo, weaponDataClone.RemainAmmo);
        weaponAnimation.RecoilFire();
    }

}
