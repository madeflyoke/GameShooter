using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public static class EventManager 
{
    public static event Action<object, bool> InputEvent;
    public static event Action<int, int> AmmoChangedEvent;
    public static event Action<int, RaycastHit> ShotEnemyEvent;
    public static event Action EnemyDieEvent;
    public static event Action <RaycastHit>ShootAnimationEvent;
    public static event Action <int,int> PlayerHitEvent;
    public static event Action EndGameEvent;    

    public static void CallOnInput(object inputclass, bool isPressed)
    {
        InputEvent?.Invoke(inputclass,isPressed);
    }
    public static void CallOnEndGameEvent()
    {
        EndGameEvent?.Invoke();
        SceneManager.LoadSceneAsync(2);
    }
    public static void CallOnPlayerHit(int damage, int remainHealth)
    {
        PlayerHitEvent?.Invoke(damage,remainHealth);
    }

    public static void CallOnShoot(RaycastHit hit)
    {
        ShootAnimationEvent?.Invoke(hit);
    }
    public static void CallOnAmmoChanged (int currentAmmo, int remainAmmo)
    {
        AmmoChangedEvent?.Invoke(currentAmmo, remainAmmo);
    }

    public static void CallOnShotEnemy(int damage, RaycastHit target)
    {
        ShotEnemyEvent?.Invoke(damage, target);
    }
    public static void CallOnEnemyDie()
    {
        EnemyDieEvent?.Invoke();
    }

}
