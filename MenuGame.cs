using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); //nhap play thi boi canh chuyen sang LoadingScene
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
