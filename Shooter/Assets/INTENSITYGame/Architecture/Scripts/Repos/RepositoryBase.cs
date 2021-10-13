using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoryBase : MonoBehaviour
{
    private PlayerInfo playerInfoObj = new PlayerInfo();
    public PlayerInfo PlayerInfoObj => playerInfoObj;
}
