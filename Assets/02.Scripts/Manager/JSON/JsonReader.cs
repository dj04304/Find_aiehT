using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public PlayerSO PlayerSO;

    private void Start()
    {
        // JSON ���� ��� ����
        string jsonFilePath = "Assets/Resources/JSON/PlayerData.json";

        // JSON ���Ͽ��� ������ �б�
        string jsonText = File.ReadAllText(jsonFilePath);

        Debug.Log(jsonText);

        DummyData playerJsonData = JsonUtility.FromJson<DummyData>(jsonText);

        if (PlayerSO != null)
        {
            //PlayerSO.PlayerData = playerJsonData;
            Debug.Log("After: " + PlayerSO.PlayerData.PlayerName); 
        }
    }
}
