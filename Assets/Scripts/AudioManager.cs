using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSource;

    public static AudioManager instance;
    void Start()
    {
        instance = this;
    }

    public void PlayAudioSource(int source)
    {
        audioSource[source].Play();
    }
}
