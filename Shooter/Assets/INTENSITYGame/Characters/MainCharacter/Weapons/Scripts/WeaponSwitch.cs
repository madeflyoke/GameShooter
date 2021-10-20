using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;
    private int selectedWeapon = 0;
    public WeaponController CurrentWeapon { get; private set; }
    public List<WeaponController> weaponsControllers;
    private void Awake()
    {
        EventManager.InputEvent += InputHolder;
        foreach (GameObject item in weapons)
        {
            weaponsControllers.Add(item.GetComponent<WeaponController>());
        }
    }
    private void OnDisable()
    {
        EventManager.InputEvent -= InputHolder;
    }
    void Start()
    {
        SelectWeapon();
    }
    private void InputHolder(object input, bool isPressed)
    {
        if (input is SwitchButtonController)
        {
            selectedWeapon = selectedWeapon == 0 ? 1 : 0;
            SelectWeapon();
        }
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Alpha1))
        //{
        //    selectedWeapon = 0;
        //    SelectWeapon();

        //}

        //if (Input.GetKey(KeyCode.Alpha2) && transform.childCount >= 2)
        //{
        //    selectedWeapon = 1;
        //    SelectWeapon();
        //}

        //if (Input.GetKey(KeyCode.Alpha3))
        //    selectedWeapon = 2;
    }
    private void SelectWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeapon)
            {
                weapons[i].SetActive(true);
                CurrentWeapon = weaponsControllers[i];
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
        EventManager.CallOnAmmoChanged(CurrentWeapon.weaponDataClone.CurrentAmmo, CurrentWeapon.weaponDataClone.RemainAmmo);
    }


}
