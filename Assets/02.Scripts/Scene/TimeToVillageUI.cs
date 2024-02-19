using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToVillageUI : MonoBehaviour
{
    private SceneMoveUI _sceneMoveUI;

    private void Start()
    {
        _sceneMoveUI = GameManager.Instance.UIManager.PopupDic[UIName.SceneMoveUI].GetComponent<SceneMoveUI>();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoVillageBtn()
    {
        _sceneMoveUI.CurrentSceneName = SceneName.VillageScene;
        _sceneMoveUI.Description.text = "마을로 돌아갑니다.";
        _sceneMoveUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StayHuntingBtn()
    {
        gameObject.SetActive(false);
    }
}
