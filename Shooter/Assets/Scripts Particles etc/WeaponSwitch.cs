using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class WeaponSwitch : MonoBehaviour
{
    //public event Action<GameObject> CurrentWeaponEvent;
    private int selectedWeapon = 0;
    public PlayerInfo playerInfo;
    void Awake()
    {
       SelectWeapon();
       playerInfo.WeaponEvent += CurrentWeapon;
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
                

                //CurrentWeaponEvent?.Invoke(item.gameObject);
            }
            else
                item.gameObject.SetActive(false);
            i++;
        }
      
    }
 
    private void CurrentWeapon(GameObject weapon)
    {

    }
    
    

}
