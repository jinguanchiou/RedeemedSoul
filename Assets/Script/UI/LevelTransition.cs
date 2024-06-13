using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    public float FadeInTime;
    private float FadeInTime_Log;
    public float DisplayTime;
    public float FadeoutTime;
    private float FadeoutTime_Log;
    public Image image;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        Color color_1 = image.color;
        color_1.a = 0;
        image.color = color_1;
        Color color_2 = text.color;
        color_2.a = 0;
        text.color = color_2;
        FadeoutTime_Log = FadeoutTime;
        FadeInTime_Log = FadeInTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeInTime > 0)
        {
            FadeIn();
        }
        if (FadeInTime <= 0)
        {
            DisplayTime -= Time.deltaTime;
        }
        if (DisplayTime <= 0)
        {
            FadeOut();
        }
    }
    void FadeOut()
    {
        FadeoutTime -= Time.deltaTime;
        Color color_1 = image.color;
        color_1.a = FadeoutTime / FadeoutTime_Log;
        image.color = color_1;
        Color color_2 = text.color;
        color_2.a = FadeoutTime / FadeoutTime_Log;
        text.color = color_2;
    }   
    void FadeIn()
    {
        FadeInTime -= Time.deltaTime;
        Color color_1 = image.color;
        color_1.a = 1 - FadeInTime / FadeInTime_Log;
        image.color = color_1;
        Color color_2 = text.color;
        color_2.a = 1 - FadeInTime / FadeInTime_Log;
        text.color = color_2;
    }
}
