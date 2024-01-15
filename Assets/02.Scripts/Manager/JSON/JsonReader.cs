using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public PlayerSO PlayerSO;

    private void Awake()
    {
        // JSON ���� ��� ����
        string jsonFilePath = "Assets/Resources/JSON/PlayerData.json";

        // JSON ���Ͽ��� ������ �б�
        string jsonText = File.ReadAllText(jsonFilePath);

        PlayerJsonData playerJsonData = JsonUtility.FromJson<PlayerJsonData>(jsonText);

        PlayerSO.SetPlayerData(playerJsonData.PlayerData);
    }
}
