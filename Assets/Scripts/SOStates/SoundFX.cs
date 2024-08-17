using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundFX", menuName = "SoundFX", order = 0)]
public class SoundFX : ScriptableObject
{
    [SerializeField] private AudioClip m_shrinkSoundFX;
    [SerializeField] private AudioClip m_stretchSoundFX;
    [SerializeField] private AudioClip m_shrinkBeamSoundFX;
    [SerializeField] private AudioClip m_stretchBeamSoundFX;
    public AudioClip Shrink { get => m_shrinkSoundFX; private set => m_shrinkSoundFX = value; }
    public AudioClip Stretch { get => m_stretchSoundFX; private set => m_stretchSoundFX = value; }
    public AudioClip ShrinkBeam { get => m_shrinkBeamSoundFX; private set => m_shrinkBeamSoundFX = value; }
    public AudioClip StretchBeam { get => m_stretchBeamSoundFX; private set => m_stretchBeamSoundFX = value; }
}
