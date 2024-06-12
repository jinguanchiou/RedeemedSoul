using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MallManager : MonoBehaviour
{
    public Coin CoinQuantity;
    public CoinUI coinUI;
    public GameObject XX;
    public PotionInventory potionInventory;
    public Inventory SkillInventory;
    public PotionBarManger PotionBarManger;

    private void Start()
    {
        if (SkillInventory.SkillList[10].CanUse)
        {
            XX.SetActive(true);
        }
        else
        {
            XX.SetActive(false);
        }
    }
    public void BuyBigPotion()
    {
        if (CoinQuantity.CoinQuantity >= 10 && potionInventory.PotionList[2].Quantity < 15)
        {
            CoinQuantity.CoinQuantity -= 10;
            potionInventory.PotionList[2].Quantity += 1;
            coinUI.Buy();
            PotionBarManger.RefreshPotion();
        }
    }
    public void BuyNormalPotion()
    {
        if (CoinQuantity.CoinQuantity >= 5 && potionInventory.PotionList[1].Quantity < 15)
        {
            CoinQuantity.CoinQuantity -= 5;
            potionInventory.PotionList[1].Quantity += 1;
            coinUI.Buy();
            PotionBarManger.RefreshPotion();
        }
    }
    public void BuySmallPotion()
    {
        if (CoinQuantity.CoinQuantity >= 3 && potionInventory.PotionList[0].Quantity < 15)
        {
            CoinQuantity.CoinQuantity -= 3;
            potionInventory.PotionList[0].Quantity += 1;
            coinUI.Buy();
            PotionBarManger.RefreshPotion();
        }
    }
    public void BuyMallSkill()
    {
        if (CoinQuantity.CoinQuantity >= 50 && !SkillInventory.SkillList[10].CanUse)
        {
            CoinQuantity.CoinQuantity -= 50;
            SkillInventory.SkillList[10].CanUse = true;
            coinUI.Buy();
            XX.SetActive(true);
            PotionBarManger.RefreshPotion();
        }
    }
}
