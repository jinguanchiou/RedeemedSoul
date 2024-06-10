using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public int startCoinQuantity;
    public Text coinQuantity;
    public Coin CoinQuantity;
    public static int CurrentCoinQuantity;
    // Start is called before the first frame update
    void Start()
    {
        startCoinQuantity = CoinQuantity.CoinQuantity;
        CurrentCoinQuantity = startCoinQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        coinQuantity.text = CurrentCoinQuantity.ToString();
    }
    public void Buy()
    {
        startCoinQuantity = CoinQuantity.CoinQuantity;
        CurrentCoinQuantity = startCoinQuantity;
        coinQuantity.text = CurrentCoinQuantity.ToString();
    }
}
