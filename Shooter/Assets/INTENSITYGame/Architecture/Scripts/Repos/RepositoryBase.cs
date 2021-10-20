using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoryBase : MonoBehaviour
{
    private PlayerInfo playerInfo = new PlayerInfo();
    public PlayerInfo PlayerInfoObj => playerInfo;

    private PlayerSettings playerSettings = new PlayerSettings();
    public PlayerSettings PlayerSettingsObj => playerSettings;
    private void Awake()
    {
        PlayerSettingsObj.Initialize();
        Application.targetFrameRate = PlayerSettingsObj.FPSLimit;
    }

}
