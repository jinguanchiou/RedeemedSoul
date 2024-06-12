using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameingUIInventory ResetGameingUI;
    public ScenseContorller scenseContorller;
    public void NewGame()
    {
        for(int i = 0; i < scenseContorller.ScenseList.Count; i++)
        {
            scenseContorller.ScenseList[i] = false;
        }
        scenseContorller.ScenseList[1] = true;
        scenseContorller.i = 1;
        SceneManager.LoadScene(1);
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
