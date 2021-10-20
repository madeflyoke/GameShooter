using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] private RepositoryBase repositoryBase;
    [SerializeField] private Toggle isAutoAim;
    [SerializeField] private Slider weaponVolume;
    [SerializeField] private Slider SFXVolume;
    [SerializeField] private Button OKButton;
    
    private void Awake()
    {
        OKButton.onClick.AddListener(SaveSettings);
        isAutoAim.isOn = repositoryBase.PlayerSettingsObj.autoAim;
        weaponVolume.value = repositoryBase.PlayerSettingsObj.weaponVolume;
        SFXVolume.value = repositoryBase.PlayerSettingsObj.SFXVolume;
    }
    private void SaveSettings()
    {
        repositoryBase.PlayerSettingsObj.autoAim = isAutoAim.isOn;
        repositoryBase.PlayerSettingsObj.SaveAutoAimValue();
        

        repositoryBase.PlayerSettingsObj.weaponVolume = weaponVolume.value;
        repositoryBase.PlayerSettingsObj.SaveWeaponVolumeValue();

        repositoryBase.PlayerSettingsObj.SFXVolume = SFXVolume.value;
        repositoryBase.PlayerSettingsObj.SaveSFXVolumeValue();

    }
}
