using UnityEngine;
public class RayTracerController : MonoBehaviour
{
    [SerializeField] private PlayerState so_playerState;
    [Header("References")]
    [SerializeField] private GameObject m_rayBeamPrefab;
    [SerializeField] private Transform m_beamOriginAtGun;
    [SerializeField] private ExitParticlesController m_exitParticles;
    
    public void ShootRayBeam(float sign, Ray ray, Vector3 hitPoint)
    {
        if (!m_beamOriginAtGun) return;
        m_exitParticles.Emit(sign, m_beamOriginAtGun.position);

        GameObject rayBeam = Instantiate(m_rayBeamPrefab, m_beamOriginAtGun.position, m_rayBeamPrefab.transform.rotation);
        rayBeam.GetComponent<RayBeamController>().HitPoint = hitPoint;
        rayBeam.GetComponent<RayBeamController>().Sign = sign;
        rayBeam.GetComponent<RayBeamController>().ShootedRayBeam = ray;
        //? The RayBeam Gets removed when the displacement ends. Destroy(rayBeam, 2.0f);
    }
}
