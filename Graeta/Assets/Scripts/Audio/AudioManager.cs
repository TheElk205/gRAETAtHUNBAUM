using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    public float fadeInStep = 0.1f;
    public float fadeOutStep = 0.15f;

    private Sound previousMusic = null;
    private Sound currentMusic = null;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    void Update()
    {
        if (previousMusic != null)
        {
            if (previousMusic.source.volume == 0)
            {
                previousMusic = null;
            }
            else
            {
                previousMusic.source.volume -= fadeOutStep * Time.deltaTime;
                if (previousMusic.source.volume <= 0)
                {
                    previousMusic.source.volume = 0;
                }
            }
        }

        if (currentMusic.source.volume < currentMusic.volume)
        {
            currentMusic.source.volume += fadeInStep * Time.deltaTime;
            if (previousMusic.source.volume >= currentMusic.volume)
            {
                previousMusic.source.volume = currentMusic.volume;
            }
        }

    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        if (!s.isBackgroundMusic)
        {
            s.source.Play();
            return;
        }
        
        if (currentMusic == null)
        {
            currentMusic = s;
            s.source.Play();
            return;
        }

        previousMusic = currentMusic;
        currentMusic = s;
        currentMusic.source.volume = 0;
        currentMusic.source.Play();
        return;
    }
}
