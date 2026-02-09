using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public void Setup(int finalScore)
    {
        gameObject.SetActive(true);
        pointsText.text = "Points: " + finalScore.ToString("#,0");
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartButton()
    {
        Debug.Log("Restart button clicked!");
        Time.timeScale = 1f; // Resume time before reloading
        SceneManager.LoadScene("_Scene_0");
    }
    public void MainMenuButton()
    {
        Debug.Log("Main Menu button clicked!");
        Time.timeScale = 1f; // Resume time before reloading
        SceneManager.LoadScene("MainMenu");
    }
}
