using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameingUIInventory GameingUI;
    public Coin CoinQuantity;
    public void Respawn()
    {
        GameingUI.HP = GameingUI.HP_MAX;
        GameingUI.MP = GameingUI.MP_MAX;
        CoinQuantity.CoinQuantity -= 20;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
