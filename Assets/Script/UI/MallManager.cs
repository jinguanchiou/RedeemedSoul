using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MallManager : MonoBehaviour
{
    public Coin CoinQuantity;
    public CoinUI coinUI;
    public PotionInventory potionInventory;
    public Inventory SkillInventory;
    public PotionBarManger PotionBarManger;
    private int BigPotionPF;
    private int NormalPotionPF;
    private int SmallPotionPF;
    private int MallSkillPF;
    public void BuyBigPotion()
    {
        if (CoinQuantity.CoinQuantity >= 10 && BigPotionPF < 5)
        {
            CoinQuantity.CoinQuantity -= 10;
            potionInventory.PotionList[2].Quantity += 1;
            coinUI.Buy();
            PotionBarManger.RefreshPotion();
            BigPotionPF++;
        }
    }
    public void BuyNormalPotion()
    {
        if (CoinQuantity.CoinQuantity >= 5 && NormalPotionPF < 5)
        {
            CoinQuantity.CoinQuantity -= 5;
            potionInventory.PotionList[1].Quantity += 1;
            coinUI.Buy();
            PotionBarManger.RefreshPotion();
            NormalPotionPF++;
        }
    }
    public void BuySmallPotion()
    {
        if (CoinQuantity.CoinQuantity >= 3 && SmallPotionPF < 5)
        {
            CoinQuantity.CoinQuantity -= 3;
            potionInventory.PotionList[0].Quantity += 1;
            coinUI.Buy();
            PotionBarManger.RefreshPotion();
            SmallPotionPF++;
        }
    }
    public void BuyMallSkill()
    {
        if (CoinQuantity.CoinQuantity >= 50 && MallSkillPF < 1)
        {
            CoinQuantity.CoinQuantity -= 50;
            SkillInventory.SkillList[10].CanUse = true;
            coinUI.Buy();
            PotionBarManger.RefreshPotion();
            MallSkillPF++;
        }
    }
}
