using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip mainForest;
    public AudioClip bgmForest;
    public AudioClip bgmForest_2;
    public AudioClip seSlash;
    public AudioClip seSlashHit;
    public AudioClip seSnap;
    public AudioClip seGulp;
    public AudioClip DrawKnife;
    public AudioClip SwingKnife;
    public AudioClip SwordEnergy;
    List<AudioSource> audios = new List<AudioSource>();
    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            var audio = this.gameObject.AddComponent<AudioSource>();
            audios.Add(audio);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void Play(int index, string name, bool isLoop)
    {
        var clip = GetAudioClip(name);
        if(clip != null)
        {
            var audio = audios[index];
            audio.clip = clip;
            audio.loop = isLoop;
            audio.Play();
        }
    }
    public void Stop(int index)
    {
        var audio = audios[index];
        audio.mute = true;
        audio.Stop();
    }
    AudioClip GetAudioClip(string name)
    {
        switch(name)
        {
            case "mainForest":
                return mainForest;
            case "bgmForest":
                return bgmForest;
            case "bgmForest_2":
                return bgmForest_2;
            case "seSlash":
                return seSlash;
            case "seSlashHit":
                return seSlashHit;
            case "seSnap":
                return seSnap;
            case "seGulp":
                return seGulp;
            case "DrawKnife":
                return DrawKnife;
            case "SwingKnife":
                return SwingKnife;
            case "SwordEnergy":
                return SwordEnergy;
        }
        return null;
    }
}
