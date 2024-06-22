using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_4Forest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.audioManager.Play(0, "mainForest", true);
        GameManager.instance.audioManager.GetComponent<AudioSource>().pitch = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
