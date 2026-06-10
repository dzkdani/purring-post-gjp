using Hellmade.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public EazySoundAudioControls BGM;

    public void PlayBGMM()
    {
        if (BGM.audio == null)
        {
            int audioID = EazySoundManager.PlayMusic(BGM.audioclip, 1, true, false);
            BGM.audio = EazySoundManager.GetAudio(audioID);
        }
        else if (BGM.audio != null && BGM.audio.Paused)
        {
            BGM.audio.Resume();
        }
        else
        {
            BGM.audio.Play();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayBGMM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public struct EazySoundAudioControls
{
    public AudioClip audioclip;
    public Audio audio;
}
