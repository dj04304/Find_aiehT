using UnityEngine;

public class RestartUI : BaseUI
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

    private void Update()
    {
        Time.timeScale = 0f;
        GameManager.Instance.CameraManager.DisableCam();
    }

    public void GoVillageBtn()
    {
        _sceneMoveUI.CurrentSceneName = SceneName.VillageScene;
        _sceneMoveUI.Description.text = "마을로 돌아갑니다.";
        _sceneMoveUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
