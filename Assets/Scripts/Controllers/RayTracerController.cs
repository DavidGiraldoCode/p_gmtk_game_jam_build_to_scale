using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(LineRenderer))]
public class RayTracerController : MonoBehaviour
{
    [SerializeField] private Transform m_raySpawnPointAtGun;
    [SerializeField] private LineRenderer m_lineRenderer;
    private Vector3[] m_beamPoints;
    private bool m_hasShoot = false;
    [SerializeField] private float m_displacementSpeed = 1.0f;
    [SerializeField] private float m_rayReach = 30.0f;
    private float displacement = 0.0f;
    private Vector3 m_beamMagnitud;
    private Ray m_ray;
    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = 2;
        m_beamPoints = new Vector3[m_lineRenderer.positionCount];
    }
    public void ShootRayBeam(float sign)
    {
        if (!m_raySpawnPointAtGun) return;
        m_hasShoot = true;
        if (sign < 0)
        {
            m_lineRenderer.startColor = Color.yellow;
            m_lineRenderer.endColor = Color.red;
        }
        else
        {
            m_lineRenderer.startColor = Color.blue;
            m_lineRenderer.endColor = Color.green;
        }

        m_ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawLine(m_raySpawnPointAtGun.position, m_ray.direction + m_raySpawnPointAtGun.position);

    }

    private void Update()
    {
        if (!m_hasShoot) return;
        BeamDisplacementAcrossRay();
    }
    public void BeamDisplacementAcrossRay()
    {
        m_beamMagnitud = m_ray.direction.normalized * 5f;
        m_beamPoints[0] = m_raySpawnPointAtGun.position;// + m_ray.direction.normalized;// * displacement;
        m_beamPoints[1] = m_raySpawnPointAtGun.position + m_beamMagnitud * displacement;//m_ray.GetPoint(displacement);//.direction * 20.0f;
        m_lineRenderer.SetPositions(m_beamPoints);
        displacement += m_displacementSpeed * Time.deltaTime;

        //Debug.Log(displacement);
        if (displacement < m_rayReach) return;
        m_beamPoints[0] = m_raySpawnPointAtGun.position;
        m_beamPoints[1] = m_raySpawnPointAtGun.position;
        m_lineRenderer.SetPositions(m_beamPoints);
        m_hasShoot = false;
        displacement = 0;
    }
}
