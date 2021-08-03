using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;


public class PlayerInfo : MonoBehaviour
{
    public event Action<GameObject> WeaponEvent;

    [SerializeField] private GameObject currentWeapon;
    [SerializeField] WeaponSwitch weaponSwitch;

    private void GetWeaponInfo(GameObject weapon)
    {
        currentWeapon = weapon;
    }
    private void Start()
    {
       
        //weaponSwitch.CurrentWeaponEvent += GetWeaponInfo;
    }
 



}
