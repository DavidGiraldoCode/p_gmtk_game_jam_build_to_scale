using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BeamAudioFX : MonoBehaviour
{
    private AudioSource m_audioSource;
    [SerializeField] private SoundFX so_soundFX;
    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.loop = false;
        m_audioSource.volume = 0.3f;
    }

    public void PlayShrink()
    {
        m_audioSource.Stop();
        m_audioSource.clip = so_soundFX.ShrinkBeam;
        m_audioSource.Play();
    }
    public void PlayStretch()
    {
        m_audioSource.Stop();
        m_audioSource.clip = so_soundFX.StretchBeam;
        m_audioSource.Play();
    }

}
