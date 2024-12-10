using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField] AudioSource soundEffects;
    [SerializeField] AudioSource backgroundMusic;

    public AudioClip bgm;
    public AudioClip sfx1;
    public AudioClip sfx2;
    public AudioClip sfx3;
    public AudioClip sfx4;
    public AudioClip sfx5;
    public AudioClip sfx6;

    private void Start()
    {
        backgroundMusic.clip = bgm;
        backgroundMusic.Play();
    }

    public void playSound(AudioClip clip)
    { 
        soundEffects.pitch = Random.Range(0.8f,1.2f);
        soundEffects.PlayOneShot(clip);
    }
}
