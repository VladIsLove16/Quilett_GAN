using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource m_AudioSource;
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        m_AudioSource.volume = PlayerPrefs.GetFloat("MusicValue");
    }
}
