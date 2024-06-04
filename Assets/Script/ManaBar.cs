using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Text ManaText;
    public static int ManaCurrent;
    public GameingUIInventory ManaMax;

    private Image manaBar;
    // Start is called before the first frame update
    void Start()
    {
        manaBar = GetComponent<Image>();
        //HealthCurrent = HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        manaBar.fillAmount = (float)ManaCurrent / (float)ManaMax.MP_MAX;
        ManaText.text = ManaCurrent.ToString() + "/" + ManaMax.MP_MAX.ToString();
    }
}
