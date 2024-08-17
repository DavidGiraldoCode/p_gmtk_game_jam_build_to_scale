using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ScalingController : MonoBehaviour
{
    private Vector3 m_currentScale = Vector3.one;
    private Vector3 m_newScale = Vector3.one;
    private float m_newTargetScaleFactor = 0.0f;
    private float m_scallingSpeed = 0.5f;
    private bool m_isScaling = false;
    private bool m_wasTriggered = false;
    private Rigidbody m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    public void TriggerScaling(float factor)
    {
        if (m_isScaling) return;
        m_wasTriggered = true;
        m_isScaling = true;
        m_newTargetScaleFactor = factor;
        Debug.Log("Triggering scalling at " + factor);
    }

    private void FixedUpdate()
    {
        if(!m_wasTriggered) return;
        m_currentScale = m_rigidbody.transform.localScale;
        m_newScale = m_currentScale * m_newTargetScaleFactor;
        m_rigidbody.transform.localScale = m_newScale;
        m_isScaling = false;
        m_wasTriggered = false;
    }

}
