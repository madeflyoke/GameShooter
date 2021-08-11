using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponAnimation : MonoBehaviour
{

    [Header("Recoil Settings:")]
    [SerializeField] private float rotationSpeed = 6;
    [SerializeField] private float returnSpeed = 25;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector3 RecoilRotation = new Vector3();
    [SerializeField] private GameObject bulletSpawner;
    private WeaponController weaponController;
    public Animator WeaponAnimator { get => animator; set => animator = value; }
   
    private Vector3 currentRot;
    private Vector3 Rot;
    void Start()
    {
        weaponController = GetComponent<WeaponController>();
        weaponController.weaponDataClone.MuzzleFlash = Instantiate(weaponController.weaponDataClone.MuzzleFlash, bulletSpawner.transform);
    }
    void FixedUpdate()
    {
        currentRot = Vector3.Lerp(currentRot, new Vector3(0, 0, 0), returnSpeed * Time.deltaTime);
        Rot = Vector3.Lerp(Rot, currentRot, rotationSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Rot);

    }
    public void RecoilFire()
    {
        currentRot += new Vector3(RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z,RecoilRotation.z));       
    }
}
