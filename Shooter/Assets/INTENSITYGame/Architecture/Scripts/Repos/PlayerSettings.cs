using System;
using UnityEngine;

public class PlayerSettings
{
    private const string WeaponVolumeKey = "WeaponVolumeKey";
    private const string SFXVolumeKey = "SFXVolumeKey";
    private const string AutoAimKey = "AutoAimKey";

    public float weaponVolume=0.7f;
    public float SFXVolume=0.5f;
    public bool autoAim;
    public int FPSLimit=60;

    public void Initialize()
    {
        if (!PlayerPrefs.HasKey(WeaponVolumeKey))//check first init on device
        {
            SaveWeaponVolumeValue();
            SaveSFXVolumeValue();
            SaveAutoAimValue();
        }
        else
        {
            weaponVolume = PlayerPrefs.GetFloat(WeaponVolumeKey);
            SFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
            autoAim = PlayerPrefs.GetInt(AutoAimKey) == 1 ? true : false;
        }  
    }

    public void SaveWeaponVolumeValue()
    {
        PlayerPrefs.SetFloat(WeaponVolumeKey, weaponVolume);
    }

    public void SaveSFXVolumeValue()
    {
        PlayerPrefs.SetFloat(SFXVolumeKey, SFXVolume);
    }

    public void SaveAutoAimValue()
    {
        PlayerPrefs.SetInt(AutoAimKey, autoAim ? 1 : 0);
    }

}

