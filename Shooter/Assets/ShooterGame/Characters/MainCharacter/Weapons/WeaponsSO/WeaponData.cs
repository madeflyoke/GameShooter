using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons", order = 51)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private bool isAutomatic; 
    private bool isReloading = false;

    [Header("FireSettings:")]
    private float nextTimeToFire = 0f;
    [SerializeField] private int damage = 0;
    [SerializeField] private float fireRate = 1;
    [SerializeField] private float fireRange = 100;
    [SerializeField] private float force = 155;
    [Space]

    [Header("ParticleEffects:")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private TrailRenderer tracerEffect;
    [Space]

    [Header("Ammo:")]
    private int remainAmmo = 0;
    private int currentAmmo = 0;
    private int maxAmmo = 0;
    [SerializeField] private int magazineAmmo;
    [SerializeField] private float reloadTime = 2;
    [SerializeField] private int numberOfMagazines = 3;

    [Header("Sound:")]
    [SerializeField] private AudioClip shotSfx;
    [SerializeField] private AudioClip reloadSFX;
  
    public bool IsAutomatic { get => isAutomatic; }
    public bool IsReloading { get => isReloading; set => isReloading =value; }

    public float NextTimeToFire { get => nextTimeToFire; set => nextTimeToFire = value; }
    public int Damage { get => damage; }
    public float FireRate { get => fireRate; }
    public float FireRange { get => fireRange; }
    public float Force { get => force; }

    public ParticleSystem MuzzleFlash { get => muzzleFlash; set => muzzleFlash = value; }
    public GameObject ImpactEffect { get => impactEffect; }
    public TrailRenderer TracerEffect { get => tracerEffect; set => tracerEffect = value; }
    
    public int MaxAmmo { get => maxAmmo; }
    public int MagazineAmmo { get => magazineAmmo; }
    public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    public float ReloadTime { get => reloadTime; }
    public int NumberOfMagazines { get => numberOfMagazines; }
    public int RemainAmmo { get => remainAmmo; set => remainAmmo = value; }

    public AudioClip ShotSfx { get => shotSfx; }
    public AudioClip ReloadSfx { get => reloadSFX; }

    private void Awake()
    {
        currentAmmo = magazineAmmo;
        maxAmmo = magazineAmmo * numberOfMagazines; 
        remainAmmo = maxAmmo;
                
    }

}
