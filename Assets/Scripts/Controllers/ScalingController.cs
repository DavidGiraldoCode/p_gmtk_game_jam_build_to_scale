using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ScalingController : MonoBehaviour
{
    private Vector3 m_currentScale = Vector3.one;
    private Vector3 m_newScale = Vector3.one;
    private float m_newTargetScaleFactor = 0.0f;
    private float m_interpolatedValue = 0.0f;
    private float m_scallingSpeed = 0.5f;
    private bool m_isScaling = false;
    private bool m_wasTriggered = false;
    private Rigidbody m_rigidbody;

    //TODO
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;
    private float startTime;
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        startTime = Time.time;
    }
    public void TriggerScaling(float factor)
    {
        if (m_isScaling) return;
        m_currentScale = m_rigidbody.transform.localScale;
        m_wasTriggered = true;
        m_isScaling = true;
        m_newTargetScaleFactor = factor;
        Debug.Log("Triggering scalling at " + factor);
    }
    float t = 0f;
    float a = 0f;
    float x = -1f;
    private void FixedUpdate()
    {
        if (!m_wasTriggered) return;

        m_interpolatedValue = Mathf.SmoothStep(1.0f, m_newTargetScaleFactor, t);


        t += 0.1f;
        Debug.Log(t);

        m_newScale = m_currentScale * m_interpolatedValue;// m_newTargetScaleFactor;
        m_rigidbody.transform.localScale = m_newScale;

        if (t < 1) return;
        m_isScaling = false;
        m_wasTriggered = false;
        t = 0f;

    }

}
