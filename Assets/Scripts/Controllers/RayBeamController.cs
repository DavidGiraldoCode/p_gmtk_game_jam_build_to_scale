using UnityEngine;
[RequireComponent(typeof(BeamAudioFX))]
public class RayBeamController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerState so_playerState;
    [SerializeField] private BeamAudioFX m_beamAudioFX;
    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private GameObject m_impactWavesParticlesPrefab;
    [Header("Settings")]
    [SerializeField] private float m_displacementSpeed = 20.0f;
    private Vector3[] m_beamPoints;
    private Vector3 m_hitPoint;
    public Vector3 HitPoint { get => m_hitPoint; set => m_hitPoint = value;}
    private float m_sign;
    public float Sign { get => m_sign; set => m_sign = value;}
    
    private float m_displacement = 0.0f;
    private Vector3 m_beamMagnitud;
    private Ray m_ray;
    public Ray ShootedRayBeam { get => m_ray; set => m_ray = value;}
    private Vector3 m_beamDirection;
    public Vector3 BeamDirection { get => m_beamDirection; set => m_beamDirection = value;}
    private GradientColorKey[] m_skrinkColorKeys;
    private GradientColorKey[] m_stretchColorKeys;
    private GradientAlphaKey[] m_alphaColorKeys;

    private void Awake()
    {
        if (!m_beamAudioFX) m_beamAudioFX = GetComponent<BeamAudioFX>();
        if (!m_lineRenderer) m_lineRenderer = GetComponent<LineRenderer>();

        m_lineRenderer.positionCount = 2;
        m_beamPoints = new Vector3[m_lineRenderer.positionCount];

        m_skrinkColorKeys = new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.yellow, 1.0f) };
        m_stretchColorKeys = new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.blue, 1.0f) };
        m_alphaColorKeys = new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) };
    }

    private void Start()
    {
        EmitImpactParticles(HitPoint, Sign);
        if (Sign < 0)
        {
            m_lineRenderer.startColor = Color.yellow;
            m_lineRenderer.endColor = Color.red;
            m_beamAudioFX.PlayShrink();
        }
        else
        {
            m_lineRenderer.startColor = Color.blue;
            m_lineRenderer.endColor = Color.green;
            m_beamAudioFX.PlayStretch();
        }
        m_ray = ShootedRayBeam;
    }

    private void Update()
    {
        BeamTailDisplacementAcrossRay();
    }
    public void BeamTailDisplacementAcrossRay()
    {
        //m_beamMagnitud = m_ray.direction.normalized * so_playerState.RayReach;
        m_beamPoints[0] = gameObject.transform.position + BeamDirection.normalized * m_displacement;//m_ray.direction.normalized * m_displacement;
        m_beamPoints[1] = m_hitPoint;//m_beamOriginAtGun.position + m_beamMagnitud;
        
        m_lineRenderer.SetPositions(m_beamPoints);
        
        m_displacement += m_displacementSpeed * Time.deltaTime;
        
        //Debug.Log(displacement);
        if (m_displacement <  BeamDirection.magnitude) return;//so_playerState.RayReach) return;
        Destroy(gameObject);
    }
    public void EmitImpactParticles(Vector3 hitPoint, float sign)
    {
        if (!m_impactWavesParticlesPrefab) return;

        GameObject impactParticles = Instantiate(m_impactWavesParticlesPrefab, hitPoint, m_impactWavesParticlesPrefab.transform.rotation);

        //* Defining color

        var impactWavesCol = impactParticles.GetComponent<ParticleSystem>().colorOverLifetime;

        impactWavesCol.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(sign > 0 ? m_stretchColorKeys : m_skrinkColorKeys, m_alphaColorKeys);

        impactWavesCol.color = grad;

        //Debug.Log(impactParticles);
        Destroy(impactParticles, impactParticles.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
    }


}
