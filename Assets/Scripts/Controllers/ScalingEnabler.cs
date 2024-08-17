using UnityEngine;

[RequireComponent(typeof(ScalingAudioFX))]
[RequireComponent(typeof(ScalingController))]
public class ScalingEnabler : MonoBehaviour
{
    private ScalingController m_scalingController;
    private ScalingAudioFX m_scalingAudioFX;

    public ScalingController ScalingController { get => m_scalingController; }
    public ScalingAudioFX ScalingAudioFX { get => m_scalingAudioFX; }
    private void Awake()
    {
        m_scalingController = GetComponent<ScalingController>();
        m_scalingAudioFX = GetComponent<ScalingAudioFX>();
    }
}
