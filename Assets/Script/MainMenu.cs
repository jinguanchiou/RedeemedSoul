using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameingUIInventory ResetGameingUI;
    public ScenseContorller scenseContorller;
    public Coin coin;
    public ConversationInventory conversation;
    public Inventory inventory;
    public PotionInventory potion;
    public void NewGame()
    {
        if (scenseContorller.i == 0)
        {
            for (int i = 0; i < scenseContorller.ScenseList.Count; i++)
            {
                scenseContorller.ScenseList[i] = false;
            }
            for (int i = 0; i < conversation.Conversation.Count; i++)
            {
                if (conversation.Conversation[i] != null)
                {
                    conversation.Conversation[i].LevelAlreadyTold = false;
                }
            }
            for (int i = 7; i < inventory.SkillList.Count; i++)
            {
                if (inventory.SkillList[i] != null)
                {
                    inventory.SkillList[i].CanUse = false;
                }
            }
            for (int i = 0; i < inventory.WorkingSkill.Count; i++)
            {
                if (inventory.WorkingSkill[i] != null)
                {
                    inventory.WorkingSkill[i] = null;
                }
            }
            for (int i = 0; i < potion.PotionList.Count; i++)
            {
                potion.PotionList[i].Quantity = 0;
            }
            coin.CoinQuantity = 0;
            scenseContorller.ScenseList[1] = true;
            scenseContorller.i = 1;
            ResetGameingUI.HP = ResetGameingUI.HP_Log;
            ResetGameingUI.MP = ResetGameingUI.MP_Log;
            SceneManager.LoadScene(1);
        }
    }
    public void Resetall()
    {
        for (int i = 0; i < scenseContorller.ScenseList.Count; i++)
        {
            scenseContorller.ScenseList[i] = false;
        }
        for (int i = 0; i < conversation.Conversation.Count; i++)
        {
            if (conversation.Conversation[i] != null)
            {
                conversation.Conversation[i].LevelAlreadyTold = false;
            }
        }
        for (int i = 7; i < inventory.SkillList.Count; i++)
        {
            if (inventory.SkillList[i] != null)
            {
                inventory.SkillList[i].CanUse = false;
            }
        }
        for (int i = 0; i < inventory.WorkingSkill.Count; i++)
        {
            if (inventory.WorkingSkill[i] != null)
            {
                inventory.WorkingSkill[i] = null;
            }
        }
        for (int i = 0; i < potion.PotionList.Count; i++)
        {
            potion.PotionList[i].Quantity = 0;
        }
        coin.CoinQuantity = 0;
        scenseContorller.ScenseList[1] = true;
        scenseContorller.i = 1;
        ResetGameingUI.HP = ResetGameingUI.HP_Log;
        ResetGameingUI.MP = ResetGameingUI.MP_Log;
        SceneManager.LoadScene(1);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(scenseContorller.i);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
