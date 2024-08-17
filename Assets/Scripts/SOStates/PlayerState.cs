using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "PlayerState", order = 0)]
public class PlayerState : ScriptableObject
{
    [SerializeField] private int m_currentScaleFactor = 0;
    [SerializeField] private int m_numberOfScalingFactors = 3;
    [SerializeField] private float m_rateOfScale = 0.25f;
    [SerializeField] private float[] m_scaleFactors;
    public int CurrentScaleFactor //TODO this will be removed, this variables is change with a method
    {
        get => m_currentScaleFactor;
        set
        {
            m_currentScaleFactor = value > m_numberOfScalingFactors - 1 ? m_currentScaleFactor : value;
        }
    }
    public void Init()
    {
        m_currentScaleFactor = 0;
        m_scaleFactors = new float[m_numberOfScalingFactors];
        for (int i = 0; i < m_numberOfScalingFactors; i++)
        {
            m_scaleFactors[i] = i * m_rateOfScale + m_rateOfScale;
        }
    }

    public void ChangeFactor(int sign)
    {
        if (sign < 0 && m_currentScaleFactor > 0)
        {
            m_currentScaleFactor--;
        }
        else if (sign > 0 && m_currentScaleFactor < m_numberOfScalingFactors - 1)
        {
            m_currentScaleFactor++;
        }
    }
}
