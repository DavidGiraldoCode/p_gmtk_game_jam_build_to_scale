using UnityEngine;
public class RayTracerController : MonoBehaviour
{
    [SerializeField] private PlayerState so_playerState;
    [SerializeField] private Transform m_beamOriginAtGun;
    //TODO --------- Place in an independent RayBeam GO
    [SerializeField] private BeamAudioFX m_beamAudioFX;
    private Vector3[] m_beamPoints;
    private Vector3 m_hitPoint;
    //Add RayBeamPrefab
    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private BeamImpactParticle m_beamImpactParticle;
    //TODO ----------

    [SerializeField] private ExitParticlesController m_exitParticles;

    private bool m_hasShoot = false;
    [SerializeField] private float m_displacementSpeed = 20.0f;
    [SerializeField] private float m_rayReach = 30.0f; //TODO set this in PlayerState
    private float m_displacement = 0.0f;
    private Vector3 m_beamMagnitud;
    private Ray m_ray;
    private void Awake()
    {
        m_lineRenderer.positionCount = 2; //TODO move to independent RayBeam GO
        m_beamPoints = new Vector3[m_lineRenderer.positionCount]; //TODO move to independent RayBeam GO
        //m_displacementSpeed = 20.0f;//so_playerState.CoolDown; //TODO move to independent RayBeam GO
    }
    public void ShootRayBeam(float sign, Ray ray, Vector3 hitPoint)
    {
        if (!m_beamOriginAtGun) return;
        m_hasShoot = true;
        m_exitParticles.Emit(sign, m_beamOriginAtGun.position);

        //TODO place all of this inside independe RayBeam GO
        m_hitPoint = hitPoint;
        EmitImpactParticles(hitPoint);
        if (sign < 0)
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
        //TODO ---------------------
        //? For testing, this ray belongs to anything the user click on the screen. Not the center
        m_ray = ray;//Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawLine(m_beamOriginAtGun.position, m_ray.direction + m_beamOriginAtGun.position);

    }

    private void FixedUpdate()
    {
        if (!m_hasShoot) return;
        BeamTailDisplacementAcrossRay();
    }
    //TODO WIP
    [SerializeField] GameObject m_beamHeadIndicator;
    public void BeamTailDisplacementAcrossRay()
    {
        m_beamMagnitud = m_ray.direction.normalized * m_rayReach;
        m_beamPoints[0] = m_beamOriginAtGun.position + m_ray.direction.normalized * m_displacement;
        m_beamPoints[1] = m_hitPoint;//m_beamOriginAtGun.position + m_beamMagnitud;
        m_lineRenderer.SetPositions(m_beamPoints);
        m_displacement += m_displacementSpeed * Time.fixedDeltaTime;

        m_beamHeadIndicator.transform.position = m_beamPoints[1];
        //Debug.Log(displacement);
        if (m_displacement < m_rayReach) return;
        m_beamPoints[0] = m_beamOriginAtGun.position;
        m_beamPoints[1] = m_beamOriginAtGun.position;
        m_lineRenderer.SetPositions(m_beamPoints);
        m_hasShoot = false;
        m_displacement = 0;
    }
    //TODO --------- place inside the RayBeam independent GM
    [SerializeField] private GameObject m_impactWavesParticlesPrefab;
    public void EmitImpactParticles(Vector3 hitPoint)
    {
        if (!m_impactWavesParticlesPrefab) return;

        GameObject impactParticles = Instantiate(m_impactWavesParticlesPrefab, hitPoint, m_impactWavesParticlesPrefab.transform.rotation);

        Debug.Log(impactParticles);
        Destroy(impactParticles, impactParticles.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
    }
    //TODO -----
    public void BeamHeadDisplacementAcrossRay()
    {
        //NOTE: using a particle material yields a very interesting look, as if it were a plasma blast
        m_beamMagnitud = m_ray.direction.normalized * 5f;
        m_beamPoints[0] = m_beamOriginAtGun.position;// + m_ray.direction.normalized;// * displacement;
        m_beamPoints[1] = m_beamOriginAtGun.position + m_beamMagnitud * m_displacement;//m_ray.GetPoint(displacement);//.direction * 20.0f;
        m_lineRenderer.SetPositions(m_beamPoints);
        m_displacement += m_displacementSpeed * Time.deltaTime;

        m_beamHeadIndicator.transform.position = m_beamPoints[1];
        //Debug.Log(displacement);
        if (m_displacement < m_rayReach) return;
        m_beamPoints[0] = m_beamOriginAtGun.position;
        m_beamPoints[1] = m_beamOriginAtGun.position;
        m_lineRenderer.SetPositions(m_beamPoints);
        m_hasShoot = false;
        m_displacement = 0;
    }
}
