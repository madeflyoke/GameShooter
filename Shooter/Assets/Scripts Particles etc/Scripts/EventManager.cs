using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class EventManager 
{
    public static event Action<int, int> AmmoChangedEvent;

    public static void CallOnAmmoChanged (int currentAmmo, int remainAmmo)
    {
        AmmoChangedEvent?.Invoke(currentAmmo, remainAmmo);

    }


}
