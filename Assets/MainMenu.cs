using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("_Scene_0");
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
}
