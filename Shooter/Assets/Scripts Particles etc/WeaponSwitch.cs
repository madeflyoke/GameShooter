using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponSwitch : MonoBehaviour
{

    private int selectedWeapon = 0;
    public event Action<Weapon> SelectedWeaponEvent;

    //[Header("Weapon:")]
    //public Weapon currentWeapon;
    [Space]
    [Header("Animation:")]
    public Animator _animator;
    void Awake()
    {

       SelectWeapon();
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
            SelectWeapon();

        }
            
        if (Input.GetKey(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
            SelectWeapon();
        }
       

        //if (Input.GetKey(KeyCode.Alpha3))
        //    selectedWeapon = 2;
    }
    private void SelectWeapon()
    {
        
        int i = 0;   

        foreach (Transform item in transform)
        {

            if (i == selectedWeapon)
            {             
                item.gameObject.SetActive(true);
                //currentWeapon = item.gameObject.GetComponentInChildren<Weapon>();
                SelectedWeaponEvent?.Invoke(item.gameObject.GetComponentInChildren<Weapon>());
            }
            else
                item.gameObject.SetActive(false);
            i++;
        }   

    }

}
