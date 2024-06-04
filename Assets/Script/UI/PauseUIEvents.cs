using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIEvents : MonoBehaviour
{
    public void ReturnToMainMenuButtonClicked()
    {
        SceneManager.LoadScene("Menu");
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
    }
}
