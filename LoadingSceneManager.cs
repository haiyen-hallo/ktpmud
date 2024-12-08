using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public TextMeshProUGUI LoadingScene;
    private void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2); // LoadingScene to Lv1
        asyncLoad.allowSceneActivation = false; // dung chuyen boi canh
        while (asyncLoad.progress < 0.9f) // asyncLoad.progress max is 0.9f 
        {
            LoadingScene.text = "Loading..." + Mathf.RoundToInt(asyncLoad.progress * 100);
            yield return null; // keep waiting while scene is loading
        }
        LoadingScene.text = "Loading...";
        yield return new WaitForSeconds(1.5f);
        asyncLoad.allowSceneActivation = true;
    }
}
