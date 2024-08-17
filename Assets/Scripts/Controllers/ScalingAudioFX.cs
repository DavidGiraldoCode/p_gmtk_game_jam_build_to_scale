using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class ScalingAudioFX : MonoBehaviour
{
    private AudioSource m_audioSource;
    [SerializeField] private SoundFX so_soundFX;
    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.loop = false;
        m_audioSource.volume = 0.1f;
    }

    public void PlayShrink(float pitch)
    {
        m_audioSource.Stop();
        m_audioSource.pitch = pitch;
        m_audioSource.clip = so_soundFX.Shrink;
        m_audioSource.Play();
    }
    public void PlayStretch(float pitch)
    {
        m_audioSource.Stop();
        m_audioSource.pitch = pitch;
        m_audioSource.clip = so_soundFX.Stretch;
        m_audioSource.Play();
    }
}
