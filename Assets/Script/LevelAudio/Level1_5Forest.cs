using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_5Forest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.audioManager.Play(0, "bgmForest_2", true);
        GameManager.instance.audioManager.GetComponent<AudioSource>().pitch = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
