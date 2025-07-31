using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public List<AudioClip> ListSound;
    public AudioSource BackgroundSound;
    public AudioSource FireSound;
    public AudioSource GetSoundScore;
    public static SoundController Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BackgroundSound = gameObject.AddComponent<AudioSource>();
        FireSound = gameObject.AddComponent<AudioSource>();
        GetSoundScore = gameObject.AddComponent<AudioSource>();
        PlayAudioSource();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayFireAudio()
    {
        FireSound.clip = ListSound[1];

        FireSound.Play();
    }
    public void PlaySoundGetScore()
    {
        GetSoundScore.clip = ListSound[2];
        GetSoundScore.Play();
    }
    void PlayAudioSource()
    {
        BackgroundSound.clip = ListSound[0];
        BackgroundSound.loop = true;
        BackgroundSound.Play();
    }
}
