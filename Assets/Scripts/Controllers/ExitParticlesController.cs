using UnityEngine;
public class ExitParticlesController : MonoBehaviour
{
    [SerializeField] private GameObject m_exitSparksParticlesPrefab;
    [SerializeField] private GameObject m_exitWavesParticlesPrefab;
    [SerializeField] private ParticleSystem exitSparksParticles;
    [SerializeField] private ParticleSystem exitWavesParticles;
    private GradientColorKey[] m_skrinkColorKeys;
    private GradientColorKey[] m_stretchColorKeys;
    private GradientAlphaKey[] m_alphaColorKeys;
    private void Awake()
    {
        m_skrinkColorKeys = new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.yellow, 1.0f) };
        m_stretchColorKeys = new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.blue, 1.0f) };
        m_alphaColorKeys = new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) };
    }
    public void Emit(float sign, Vector3 particlePosition)
    {
        if (!exitSparksParticles && !exitWavesParticles) return;

        var sparksCol = exitSparksParticles.colorOverLifetime;
        var wavesCol = exitWavesParticles.colorOverLifetime;

        sparksCol.enabled = true;
        wavesCol.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(sign > 0 ? m_stretchColorKeys : m_skrinkColorKeys, m_alphaColorKeys);
        
        sparksCol.color = grad;
        wavesCol.color = grad;

        exitSparksParticles.Play();
        exitWavesParticles.Play();
        // exitSparksParticles = Instantiate(m_exitSparksParticlesPrefab, particlePosition, Quaternion.identity);
        // exitWavesParticles = Instantiate(m_exitWavesParticlesPrefab, particlePosition, Quaternion.identity);
        // Destroy(exitSparksParticles, exitSparksParticles.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
        // Destroy(exitWavesParticles, exitSparksParticles.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);

    }

}