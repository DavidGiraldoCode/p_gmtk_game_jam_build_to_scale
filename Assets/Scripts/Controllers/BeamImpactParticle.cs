using UnityEngine;

public class BeamImpactParticle : MonoBehaviour
{
    [SerializeField] private GameObject m_impactWavesParticlesPrefab;
     [SerializeField] private GameObject m_impactDebugPrefab;
    private void OnTriggerEnter(Collider other)
    {
        //if(!other) return;
        Debug.Log(other.gameObject.name);
        if (!m_impactWavesParticlesPrefab && !m_impactDebugPrefab) return;

        Vector3 impactPosition = GetComponent<Collider>().transform.position;
        Debug.Log(impactPosition);

        GameObject impactParticles = Instantiate(m_impactWavesParticlesPrefab, impactPosition, m_impactWavesParticlesPrefab.transform.rotation);
        
        Instantiate(m_impactDebugPrefab, impactPosition, m_impactDebugPrefab.transform.rotation);
        Debug.Log(impactParticles);
        Destroy(impactParticles, impactParticles.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
    }
}
