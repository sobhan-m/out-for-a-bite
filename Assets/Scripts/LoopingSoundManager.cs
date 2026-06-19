using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LoopingSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Stop();
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public void PlayAudio(bool shouldPlay)
    {
        if (shouldPlay)
        {
            PlayAudio();
        } else
        {
            StopAudio();
        }
    }
}
