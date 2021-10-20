using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    public WeaponData weaponDataClone { get; private set; }

    private RepositoryBase repositoryBase;
    private WeaponAnimation weaponAnimation;
    private AudioSource audioSource;
    private Camera bulletSpawner;
    private bool CanShoot, CanReload;
    private void Awake()
    {
        repositoryBase = FindObjectOfType<RepositoryBase>();
        weaponDataClone = Instantiate(weaponData);
        weaponAnimation = GetComponent<WeaponAnimation>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = repositoryBase.PlayerSettingsObj.weaponVolume;
        bulletSpawner = GetComponentInParent<Camera>();
    }
    private void OnEnable()
    {
        weaponAnimation.WeaponAnimator.Play("SwitchingOnAnim");
        weaponDataClone.IsReloading = false;
        weaponAnimation.WeaponAnimator.SetBool("Reloading", false);
        EventManager.InputEvent += InputHandler;
    }
    private void OnDisable()
    {
        EventManager.InputEvent -= InputHandler;
    }
    private void InputHandler(object obj, bool isPressed)
    {
        if (obj is FireButtonController)
        {
            CanShoot = isPressed;
        }
        else if (obj is ReloadButtonController)
        {
            CanReload = isPressed;
        }
    }

    void Update()
    {
        if (weaponDataClone.IsReloading)
            return;

        if (weaponDataClone.CurrentAmmo <= 0 && weaponDataClone.RemainAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (CanReload)
        {
            if (weaponDataClone.CurrentAmmo != weaponDataClone.MagazineAmmo && weaponDataClone.RemainAmmo > 0)                                                                              
            {
                StartCoroutine(Reload());
                return;
            }
        }
        if (CanShoot)
        {
            if (Time.time >= weaponDataClone.NextTimeToFire && weaponDataClone.CurrentAmmo > 0)
            {
                weaponDataClone.NextTimeToFire = Time.time + 1f / weaponDataClone.FireRate;
                if (repositoryBase.PlayerSettingsObj.autoAim)
                {
                    EnemyAutoAim();
                }          
                Shoot();
                if (!weaponDataClone.IsAutomatic)
                    CanShoot = false;
            }
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
        CanReload = false;
    }

    private void EnemyAutoAim()
    {
        if (Physics.SphereCast(bulletSpawner.transform.position, 2f, bulletSpawner.transform.forward, out RaycastHit sphereHit))
        {
            if (sphereHit.collider.gameObject.CompareTag("Enemy") || sphereHit.collider.gameObject.CompareTag("EnemyHead"))
            {
                if (sphereHit.collider.attachedRigidbody.isKinematic)
                {
                    bulletSpawner.transform.LookAt(sphereHit.collider.gameObject.transform.position);
                }             
            }
        }
    }
    public void Shoot()
    {
        audioSource.PlayOneShot(weaponDataClone.ShotSfx);
        if (Physics.Raycast(bulletSpawner.transform.position, bulletSpawner.transform.forward, out RaycastHit hit, weaponDataClone.FireRange))
        {
            if (hit.rigidbody != null)
            {
                EventManager.CallOnShoot(hit);
                hit.rigidbody.AddForce(-hit.normal * weaponDataClone.Force);
                if (hit.rigidbody.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("hit enemy");
                    EventManager.CallOnShotEnemy(weaponDataClone.Damage, hit);
                }
                if (hit.rigidbody.gameObject.CompareTag("EnemyHead"))
                {
                    EventManager.CallOnShotEnemy(weaponDataClone.Damage * 3, hit);
                }
            }
        }
        weaponDataClone.CurrentAmmo--;
        EventManager.CallOnAmmoChanged(weaponDataClone.CurrentAmmo, weaponDataClone.RemainAmmo);
    }

}
