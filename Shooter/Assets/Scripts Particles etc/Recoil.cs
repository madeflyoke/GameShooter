using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Recoil : MonoBehaviour
{
    private Weapon fireRate;
    private float nextTimeToFire;

    [Header("Recoil Settings:")]
    public float rotationSpeed = 6;
    public float returnSpeed = 25;
    [Space]

    public Vector3 RecoilRotation = new Vector3();
    private Vector3 currentRot;
    private Vector3 Rot;
    void Start()
    {
        fireRate = GetComponent<Weapon>();       
    }
    void FixedUpdate()
    {
        currentRot = Vector3.Lerp(currentRot, new Vector3(0, 0, 0), returnSpeed * Time.deltaTime);
        Rot = Vector3.Lerp(Rot, currentRot, rotationSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Rot);

    }
    public void Fire()
    {
        currentRot += new Vector3(RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z,RecoilRotation.z));       
    }
}
