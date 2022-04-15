using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLamp : MonoBehaviour
{
    [SerializeField] private RepositoryBase repositoryBase;
    [SerializeField] private WeaponSwitch weapons;
    [SerializeField] private AudioClip ammoCollectSFX;
    [SerializeField] private Material glass;
    [SerializeField] private float reloadTime;
    private AudioSource audioSource;
    private bool lampAccess = true;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = repositoryBase.PlayerSettingsObj.SFXVolume;
    }
    private void OnDisable()
    {
        glass.color = Color.white;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && lampAccess)
        {
            foreach (WeaponController item in weapons.weaponsControllers)
            {
                item.weaponDataClone.CurrentAmmo = item.weaponDataClone.MagazineAmmo;
                item.weaponDataClone.RemainAmmo = item.weaponDataClone.MaxAmmo;
            }
            EventManager.CallOnAmmoChanged(weapons.CurrentWeapon.weaponDataClone.CurrentAmmo,
                                           weapons.CurrentWeapon.weaponDataClone.RemainAmmo);
            audioSource.PlayOneShot(ammoCollectSFX);
            lampAccess = false;
            StartCoroutine(reloadLamp());
            glass.color = Color.red;

        }
    }
    IEnumerator reloadLamp()
    {
        yield return new WaitForSeconds(reloadTime);
        lampAccess = true;
        glass.color = Color.white;

    }


}
