using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameingUIInventory ResetGameingUI;
    public void NewGame()
    {
        SceneManager.LoadScene(3);
    }
    public void Resetall()
    {
        ResetGameingUI.HP = ResetGameingUI.HP_Log;
        ResetGameingUI.MP = ResetGameingUI.MP_Log;
    }
    public void LoadGame()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
