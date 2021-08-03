using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interface : MonoBehaviour
{
    [SerializeField]
    private PlayerInfo PlayerInfo;
    [SerializeField]
    private Text CurrentAmmoUI;
    //private PlayerInfo PlayerInfoUI;
    [SerializeField]
    private Text RemainAmmoUI;

    void Awake()
    {

        //weaponClass.ShootEvent += CurrentMaxAmmoUI;
        //PlayerInfoUI = FindObjectOfType<PlayerInfo>();
        
    }
    void Update()
    {
        //CurrentAmmoUI.text = PlayerInfo.CurrentWeapon.currentAmmo.ToString();
        //RemainAmmoUI.text = PlayerInfo.CurrentWeapon.remainAmmo.ToString();

        //CurrentAmmoUI.text = PlayerInfoUI.CurrentWeapon.currentAmmo.ToString();
        //MaxAmmoUI.text = ("/" + PlayerInfoUI.CurrentWeapon.remainAmmo.ToString());
    }

    void CurrentMaxAmmoUI(int currentammoUI, int remainammoUI)
    {
        CurrentAmmoUI.text = currentammoUI.ToString();
        RemainAmmoUI.text = remainammoUI.ToString();

    }
}
