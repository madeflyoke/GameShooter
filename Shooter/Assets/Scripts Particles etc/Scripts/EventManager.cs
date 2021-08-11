using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class EventManager 
{
    public static event Action<int, int> AmmoChangedEvent;
    public static event Action<float> ShotAndHitEvent;
    public static event Action EnemyDieEvent;
    public static void CallOnAmmoChanged (int currentAmmo, int remainAmmo)
    {
        AmmoChangedEvent?.Invoke(currentAmmo, remainAmmo);
    }

    public static void CallOnShotAndHit(float damage)
    {
        ShotAndHitEvent?.Invoke(damage);
    }
    public static void CallOnEnemyDie()
    {
        EnemyDieEvent?.Invoke();
    }

}
