using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OR_SceneManager : MonoBehaviour
{
    //ASync
    private AsyncOperation async;           //AsyncOperation

    //String
    public string SceneName;      //読み込むシーン名

    //bool
    private bool isLoading;

    //GameObject
    [SerializeField] private GameObject LoadingUI;  //ローディングUI

    //Image
    [SerializeField] private Image LoadingBar;      //NowLoadingのプログレスバーの指定

    //Text
    [SerializeField] private Text ProgressText;     //プログレステキスト

    private void Awake()
    {
        isLoading = false;
    }
    //次のシーンへ移動するシステム
    //次のシーンへ移動したいときは NextSceneLoad()関数をご使用ください。
    //↓ 使い方 ↓
    //1 -  そのシーンに空のゲームオブジェクトを配置し、このスクリプトを設定します。
    //2 -  SceneNameに読み込ませたいシーンの名前を入力します。
    //3 -  そのオブジェクトからこのスクリプトを読み込んで 任意のタイミングで NextSceneLoadを実行します
    public void NextSceneLoad()
    {
        if (!isLoading)
        {
            LoadingUI.SetActive(true);       
            StartCoroutine("LoadAsyncSceneSystem");
            isLoading = true;
        }
    }


    //ローディングシステム 
    IEnumerator LoadAsyncSceneSystem()
    {
        async = SceneManager.LoadSceneAsync(SceneName);
        async.allowSceneActivation = false;
        //シーンが読み終わるまで待つ
        while (async.progress < 0.9f)
        {
            ProgressText.text = (async.progress * 100).ToString("0") + "%";
            LoadingBar.fillAmount = async.progress;
            yield return null;
        }
        ProgressText.text = "100%";
        LoadingBar.fillAmount = 1.0f;
        yield return new WaitForSeconds(1.25f); //待ち時間
        async.allowSceneActivation = true;
    }
    void stopMusic()
    {
    }
}
