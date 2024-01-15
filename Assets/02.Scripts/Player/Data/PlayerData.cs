using System;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class PlayerData
{
    //[SerializeField]
    //[JsonProperty("PlayerName")]
    //private string _playerName;

    //[SerializeField]
    //[JsonProperty("PlayerLevel")]
    //private int _playerLevel;

    //[SerializeField]
    //[JsonProperty("PlayerMaxHealth")]
    //private float _playerMaxHealth;

    //[SerializeField]
    //[JsonProperty("PlayerMaxStamina")]
    //private float _playerMaxStamina;

    //[SerializeField]
    //[JsonProperty("PlayerAttack")]
    //private float _playerAttack;

    //[SerializeField]
    //[JsonProperty("PlayerDef")]
    //private float _playerDef;

    //[SerializeField]
    //[JsonProperty("PlayerExp")]
    //private float _playerExp;

    //[SerializeField]
    //[JsonProperty("PlayerGold")]
    //private float _playerGold;

    [SerializeField] private string PlayerName;
    [SerializeField] private int PlayerLevel;
    [SerializeField] private int PlayerMaxHealth;
    [SerializeField] private int PlayerMaxStamina;
    [SerializeField] private int PlayerAttack;
    [SerializeField] private int PlayerDef;
    [SerializeField] private int PlayerExp;
    [SerializeField] private int PlayerGold;

    //[SerializeField]  private string _playerName;
    //[SerializeField]  private int _playerLevel;
    //[SerializeField]  private int _playerMaxHealth;
    //[SerializeField]  private int _playerMaxStamina;
    //[SerializeField]  private int _playerAttack;
    //[SerializeField]  private int _playerDef;
    //[SerializeField]  private int _playerExp;
    //[SerializeField]  private int _playerGold;

}

[Serializable]
public class PlayerJsonData
{
    [SerializeField] public PlayerData PlayerData;
}