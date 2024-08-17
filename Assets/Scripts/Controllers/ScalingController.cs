using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class ScalingController : MonoBehaviour
{
    private Vector3 m_currentScale = Vector3.one;
    private Vector3 m_newScale = Vector3.one;
    private float m_newTargetScaleFactor = 0.0f;
    private float m_interpolatedValue = 0.0f;
    private bool m_isScaling = false;
    private bool m_wasTriggered = false;
    private Rigidbody m_rigidbody;
    [Header("Scaling controllers")]

    [SerializeField] float m_interpolationSpeed = 5.0f;
    private float m_t = 0.0f;

    //TODO
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    public void TriggerScaling(float factor)
    {
        if (m_isScaling) return;

        m_currentScale = m_rigidbody.transform.localScale;
        m_newTargetScaleFactor = factor;

        m_wasTriggered = true;
        m_isScaling = true;
        Debug.Log("Triggering scalling at " + factor);
    }

    private void FixedUpdate()
    {
        if (!m_wasTriggered) return;
        Scale();

    }

    private void Scale()
    {
        m_interpolatedValue = Mathf.SmoothStep(1.0f, m_newTargetScaleFactor, m_t);
        m_t += m_interpolationSpeed * Time.fixedDeltaTime;

        m_newScale = m_currentScale * m_interpolatedValue;// m_newTargetScaleFactor;
        m_rigidbody.transform.localScale = m_newScale;

        if (m_t < 1) return;
        //if (m_interpolatedValue < m_newTargetScaleFactor) return;
        m_isScaling = false;
        m_wasTriggered = false;
        m_t = 0.0f;
    }

}
