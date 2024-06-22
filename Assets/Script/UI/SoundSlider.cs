using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Slider soundSlider;
    private GameObject audioObject;
    private AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        soundSlider = GetComponent<Slider>();
        audioObject = GameObject.Find("AudioManager(Clone)");

        if (audioObject != null)
        {
            audioSources = audioObject.GetComponents<AudioSource>();
        }
        if (audioSources != null && audioSources.Length > 0)
        {
            soundSlider.value = audioSources[0].volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (soundSlider != null && audioSources != null)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = soundSlider.value;
            }
        }
    }
}