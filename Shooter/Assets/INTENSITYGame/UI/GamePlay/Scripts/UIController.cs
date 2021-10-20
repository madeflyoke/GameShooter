using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private RepositoryBase repositoryBase;
    [SerializeField] private Text CurrentAmmoUI;
    [SerializeField] private Text RemainAmmoUI;
    [SerializeField] private Slider HealthBar;
    [SerializeField] private GameObject hitCrossHair;
    private Animator animator;

    void Awake()
    {
        EventManager.AmmoChangedEvent += GetWeaponInfoUI;
        EventManager.PlayerHitEvent += GetCurrentHealthUI;
        EventManager.ShotEnemyEvent += EnemyHitUI;
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        HealthBar.maxValue = repositoryBase.PlayerInfoObj.maxHealth;
        HealthBar.value = HealthBar.maxValue;
    }
    private void OnDisable()
    {
        EventManager.AmmoChangedEvent -= GetWeaponInfoUI;
        EventManager.PlayerHitEvent -= GetCurrentHealthUI;
        EventManager.ShotEnemyEvent -= EnemyHitUI;
    }
    private void GetWeaponInfoUI(int currentAmmo, int remainAmmo)
    {
        CurrentAmmoUI.text = currentAmmo.ToString();
        RemainAmmoUI.text = remainAmmo.ToString();
    }

    private void EnemyHitUI(int itmp, RaycastHit rtmp)
    {
        StopCoroutine("CrossHairHit");
        StartCoroutine("CrossHairHit");
    }

    IEnumerator CrossHairHit()
    {
        hitCrossHair.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        hitCrossHair.SetActive(false);
    }

    private void GetCurrentHealthUI(int damage, int remainHealth)
    {
        HealthBar.value = remainHealth;
        animator.SetTrigger("GetDmg");
    }
}
