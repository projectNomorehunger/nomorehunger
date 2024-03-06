using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip attack;
    public AudioClip attackHit;
    public AudioClip hurt;
    public AudioClip death;
    public AudioClip talking;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
        
    }
}
