using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Script to control Audio logic
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager audioManager = null;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound sound in sounds)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            sound.source = audioSource;
            audioSource.playOnAwake = false;
            audioSource.clip = sound.sound;
            audioSource.volume = sound.volumn;
            audioSource.loop = sound.isLoop;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Play(string name)
    {
        Sound sound = sounds.Where(s => s.name == name).FirstOrDefault();

        if (sound != null)
        {
            sound.Play();
        }
    }

    public void Stop(string name)
    {
        Sound sound = sounds.Where(p => p.name == name && p.source.isPlaying).FirstOrDefault();

        if(sound != null)
        {
            sound.Stop();
        }
    }
}

[System.Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource source;
    public AudioClip sound;
    public string name;
    public float volumn;
    public bool isLoop;

    public void Play()
    {
        source.Play();
    }    
    
    public void Stop()
    {
        source.Stop();
    }
}