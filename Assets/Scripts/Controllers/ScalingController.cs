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

    [SerializeField] private float m_interpolationSpeed = 5.0f;
    [SerializeField] private ScalingAudioFX m_scalingAudioFX;
    private float m_t = 0.0f;
    private Vector3 m_minimumScaleThreshold = new Vector3(0.1f, 0.1f, 0.1f);
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        if (!m_scalingAudioFX)
            m_scalingAudioFX = GetComponent<ScalingAudioFX>();
        //DebuggingLocalScale();
    }
    public void TriggerScaling(float scaleFactor)
    {
        if (m_isScaling) return;
        m_currentScale = m_rigidbody.transform.localScale;

        //Scaling threshold
        if (scaleFactor >= 1.0f && m_currentScale.z >= 1.9f) return;
        //Debug.Log("------ Sorry, the geometry has suffered to much scaling! ----- ");
        //DebuggingLocalScale();  


        m_newTargetScaleFactor = scaleFactor;

        if (scaleFactor > 1.0f)
            m_scalingAudioFX.PlayStretch(scaleFactor + 0.02f);
        else
            m_scalingAudioFX.PlayShrink(scaleFactor - 0.02f);

        m_wasTriggered = true;
        m_isScaling = true;
        //Debug.Log("Triggering scalling at " + scaleFactor);
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
        RemoveGeometryWhenTooSmall();
        //DebuggingLocalScale();
    }

    private void DebuggingLocalScale()
    {
        Debug.Log("Local scale: " + gameObject.transform.localScale);  //TODO checkking for size
        Debug.Log("Lossy Scale: " + gameObject.transform.lossyScale);  //TODO checkking for size
    }

    private void RemoveGeometryWhenTooSmall()
    {
        if (gameObject.transform.localScale.magnitude < m_minimumScaleThreshold.magnitude)
            Destroy(gameObject);
    }
}
