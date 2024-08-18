using UnityEngine;
public class RayTracerController : MonoBehaviour
{
    [SerializeField] private PlayerState so_playerState;
    [SerializeField] private Transform m_beamOriginAtGun;
    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private BeamAudioFX m_beamAudioFX;
    [SerializeField] private ExitParticlesController m_exitParticles;
    private Vector3[] m_beamPoints;
    private bool m_hasShoot = false;
    private float m_displacementSpeed;
    [SerializeField] private float m_rayReach = 30.0f;
    private float displacement = 0.0f;
    private Vector3 m_beamMagnitud;
    private Ray m_ray;
    private void Awake()
    {
        m_lineRenderer.positionCount = 2;
        m_beamPoints = new Vector3[m_lineRenderer.positionCount];
        m_displacementSpeed = so_playerState.CoolDown;
    }
    public void ShootRayBeam(float sign)
    {
        if (!m_beamOriginAtGun) return;
        m_hasShoot = true;
        m_exitParticles.Emit(sign, m_beamOriginAtGun.position);
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

        m_ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
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
        m_beamPoints[0] = m_beamOriginAtGun.position + m_ray.direction.normalized * displacement;
        m_beamPoints[1] = m_beamOriginAtGun.position + m_beamMagnitud;
        m_lineRenderer.SetPositions(m_beamPoints);
        displacement += m_displacementSpeed * Time.fixedDeltaTime;

        m_beamHeadIndicator.transform.position = m_beamPoints[1];
        //Debug.Log(displacement);
        if (displacement < m_rayReach) return;
        m_beamPoints[0] = m_beamOriginAtGun.position;
        m_beamPoints[1] = m_beamOriginAtGun.position;
        m_lineRenderer.SetPositions(m_beamPoints);
        m_hasShoot = false;
        displacement = 0;
    }
    public void BeamHeadDisplacementAcrossRay()
    {
        //NOTE: using a particle material yields a very interesting look, as if it were a plasma blast
        m_beamMagnitud = m_ray.direction.normalized * 5f;
        m_beamPoints[0] = m_beamOriginAtGun.position;// + m_ray.direction.normalized;// * displacement;
        m_beamPoints[1] = m_beamOriginAtGun.position + m_beamMagnitud * displacement;//m_ray.GetPoint(displacement);//.direction * 20.0f;
        m_lineRenderer.SetPositions(m_beamPoints);
        displacement += m_displacementSpeed * Time.deltaTime;

        m_beamHeadIndicator.transform.position = m_beamPoints[1];
        //Debug.Log(displacement);
        if (displacement < m_rayReach) return;
        m_beamPoints[0] = m_beamOriginAtGun.position;
        m_beamPoints[1] = m_beamOriginAtGun.position;
        m_lineRenderer.SetPositions(m_beamPoints);
        m_hasShoot = false;
        displacement = 0;
    }
}
