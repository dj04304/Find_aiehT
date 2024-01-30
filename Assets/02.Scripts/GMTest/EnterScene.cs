using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterScene : MonoBehaviour
{
    public Image AreaImage;
    public TextMeshProUGUI AreaText;
    private string AreaInfo;

    public float WaitTime = 3f;
    public float DisableTime = 1f;

    Coroutine _co;

    private void Awake()
    {
        SceneManager.sceneLoaded += Enter;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Enter;
        _co = StartCoroutine(FadeOutImage());
    }

    private void OnDisable()
    {
        if (_co != null)
        {
            StopCoroutine(_co);
        }
        SceneManager.sceneLoaded -= Enter;
    }

    private void Enter(Scene scene, LoadSceneMode mode)
    {
        //TODO 다른방법 생각해보기
        if (scene.name == "KGM")
        {
            AreaInfo = "사냥터";
        }
        else if (scene.name == "KGM_TestVillage")
        {
            AreaInfo = "마을";
        }
        AreaText.text = AreaInfo;
        gameObject.SetActive(true);
    }

    //IEnumerator FadeOutText()
    //{
    //    _isCo = true;
    //    yield return new WaitForSeconds(WaitTime);

    //    float _elapsedTime = 0f;
    //    while (_elapsedTime < DisableTime)
    //    {
    //        AreaText.alpha = Mathf.Lerp(1f, 0f, _elapsedTime / DisableTime);

    //        _elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    gameObject.SetActive(false);
    //    _isCo = false;
    //}

    private IEnumerator FadeOutImage()
    {
        Color color = AreaImage.color;
        color.a = 1f;
        AreaText.alpha = 1f;

        yield return new WaitForSeconds(WaitTime);

        float _elapsedTime = 0f;

        while (_elapsedTime < DisableTime)
        {
            //color.a = Mathf.Lerp(1f, 0f, _elapsedTime / DisableTime);
            color.a = Mathf.Lerp(1f, 0f, _elapsedTime / DisableTime);
            AreaText.alpha = Mathf.Lerp(1f, 0f, _elapsedTime / DisableTime);

            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        _co = null;
    }

}