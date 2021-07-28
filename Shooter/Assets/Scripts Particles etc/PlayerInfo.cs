using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;


public class PlayerInfo : MonoBehaviour
{

    [SerializeField]
    private WeaponSwitch weaponSwitch;

    public Weapon CurrentWeapon;

    void CurrentWeaponMeth(Weapon selectedweapon)
    {
        CurrentWeapon = selectedweapon;
    }
    private void Awake()
    {
        weaponSwitch.SelectedWeaponEvent += CurrentWeaponMeth;

        //WeaponSwitch = FindObjectOfType<WeaponSwitch>();
        //CurrentWeapon = WeaponSwitch.currentWeapon;
    }
    private void Update()
    {
        //CurrentWeapon = WeaponSwitch.currentWeapon;        
    }
    




}
