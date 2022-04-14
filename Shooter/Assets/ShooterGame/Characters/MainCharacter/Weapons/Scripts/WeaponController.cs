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
        weaponDataClone = Instantiate(weaponData);
        weaponAnimation = GetComponent<WeaponAnimation>();
        audioSource = GetComponent<AudioSource>();
        bulletSpawner = GetComponentInParent<Camera>();

    }
    private void OnEnable()
    {
        weaponAnimation.WeaponAnimator.Play("SwitchingOnAnim");
        weaponDataClone.IsReloading = false;
        weaponAnimation.WeaponAnimator.SetBool("Reloading", false);

    }
    void Update()
    {
        if (weaponDataClone.IsReloading)
            return;
        if ((weaponDataClone.CurrentAmmo <= 0 || weaponDataClone.CurrentAmmo != weaponDataClone.MagazineAmmo
            && Input.GetKey(KeyCode.R)) && weaponDataClone.RemainAmmo > 0)
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
        weaponAnimation.WeaponAnimator.SetBool("Reloading", true);
        yield return new WaitForSeconds(weaponDataClone.ReloadTime);
        weaponAnimation.WeaponAnimator.SetBool("Reloading", false);

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
        RaycastHit hit;
        if (Physics.Raycast(bulletSpawner.transform.position, bulletSpawner.transform.forward, out hit, weaponDataClone.FireRange))
        {
            if (hit.rigidbody != null)
            {
                EventManager.CallOnShoot(hit);
                hit.rigidbody.AddForce(-hit.normal * weaponDataClone.Force);
                if (hit.rigidbody.gameObject.CompareTag("Enemy"))
                {
                    EventManager.CallOnShotEnemy(weaponDataClone.Damage, hit);
                }
                if (hit.rigidbody.gameObject.CompareTag("EnemyHead"))
                {
                    EventManager.CallOnShotEnemy(weaponDataClone.Damage*3, hit);
                }
            }
        }
        weaponDataClone.CurrentAmmo--;
        EventManager.CallOnAmmoChanged(weaponDataClone.CurrentAmmo, weaponDataClone.RemainAmmo);
    }

}
