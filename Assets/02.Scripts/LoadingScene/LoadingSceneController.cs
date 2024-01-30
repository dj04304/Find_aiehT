using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;
    [SerializeField] Image _bar;
    public GameObject Dont;

    private void Awake()
    {
        Dont = FindObjectOfType<DontDestroy>().gameObject;
    }

    public static void LoadScene(string sceneName) 
    {
        nextScene = sceneName;
        SceneManager.LoadScene(SceneName.LoadingScene);
    }
    
    private void Start()
    {
        //SceneManager.sceneLoaded += EnterScene;
        StartCoroutine(LoadSceneProcess());
    }

    void EnterScene(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != SceneName.LoadingScene)
            Dont.SetActive(true);
    }

    IEnumerator LoadSceneProcess() 
    {
        Dont.SetActive(false);
        //Async -> 비동기로 씬을 불러오면서 다른 작업 가능
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false; // 씬의 로딩이 끝나면 자동으로 넘어갈지 설정 (false -> 90퍼에서 기다리고 진행)(로딩씬의 내용이 좀 더 보이게)
            float timer = 0f;
        while(!op.isDone) 
        {
            yield return null;

            if (op.progress <0.9f)
            {
                _bar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime; //TimeScale에 영향을 받지않는 DeltaTime
                _bar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (_bar.fillAmount >= 1f)
                {
                    //Dont.SetActive(true);
                    op.allowSceneActivation = true;
                    op.completed += c => { Dont.SetActive(true); };
                    yield break;
                }
            }
        }
    }
}