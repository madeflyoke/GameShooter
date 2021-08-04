using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interface : MonoBehaviour
{

    [SerializeField]
    private Text CurrentAmmoUI;
    [SerializeField]
    private Text RemainAmmoUI;

    void Awake()
    {     
        EventManager.AmmoChangedEvent += GetWeaponInfoUI;
    }

    private void GetWeaponInfoUI(int currentAmmo, int remainAmmo)
    {

        CurrentAmmoUI.text = currentAmmo.ToString();
        RemainAmmoUI.text = remainAmmo.ToString();
    }

}
