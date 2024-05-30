using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    public float CoolDownTime;
    public float CoolDownTime_Log;
    public Image CoolDownImage;
    public Text CoolDownText;

    // Start is called before the first frame update
    void Start()
    {
        CoolDownTime_Log = CoolDownTime;
        CoolDownImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CoolDownTime >= 0)
        {
            CoolDownImage.enabled = true;
            CoolDownText.enabled = true;
            CoolDownTime -= Time.deltaTime;
            CoolDownImage.fillAmount = CoolDownTime/ CoolDownTime_Log;
            float roundedValue = Mathf.Round(CoolDownTime * 10f) / 10f;
            CoolDownText.text = roundedValue.ToString();
        }
        if(CoolDownTime < 0)
        {
            CoolDownImage.enabled = false;
            CoolDownText.enabled = false;
        }
    }
}
