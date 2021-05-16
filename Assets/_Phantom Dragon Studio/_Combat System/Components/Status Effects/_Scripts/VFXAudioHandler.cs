using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXAudioHandler : MonoBehaviour
{
AudioSource audioSource;

    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        this.audioSource.Play();
    }
}
