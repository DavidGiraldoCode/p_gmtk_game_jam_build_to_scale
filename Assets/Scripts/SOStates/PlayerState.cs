using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "PlayerState", order = 0)]
public class PlayerState : ScriptableObject
{
    [SerializeField] private int m_currentScaleFactorIndex = 0;
    [SerializeField] private int m_numberOfScalingFactors = 3;
    [SerializeField] private float m_rateOfScale = 0.25f;
    [SerializeField] private float[] m_scaleFactors;
    [SerializeField] private float m_coolDown = 1.0f;
    [SerializeField] private bool m_readyToShoot = true;
    [SerializeField]  private float m_rayReach = 30.0f;
    [SerializeField] private float m_walkingSpeed = 10.0f;
    public float WalkingSpeed { get => m_walkingSpeed; set => m_walkingSpeed = value; }
    public float RayReach { get => m_rayReach; set => m_rayReach = value;}
    public float CoolDown { get => m_coolDown; private set => m_coolDown = value; }
    public bool ReadyToShoot { get => m_readyToShoot; set => m_readyToShoot = value; }
    public float CurrentScaleFactor //TODO this will be removed, this variables is change with a method
    {
        get
        {
            return m_currentScaleFactorIndex < m_scaleFactors.Length ? m_scaleFactors[m_currentScaleFactorIndex] : 1.0f;
        }
        // set
        // {
        //     m_currentScaleFactorIndex = value > m_numberOfScalingFactors - 1 ? m_currentScaleFactorIndex : value;
        // }
    }
    public void Init()
    {
        m_currentScaleFactorIndex = 0;
        m_scaleFactors = new float[m_numberOfScalingFactors];
        for (int i = 0; i < m_numberOfScalingFactors; i++)
        {
            m_scaleFactors[i] = i * m_rateOfScale + m_rateOfScale;
        }
    }

    public void ChangeFactor(int sign)
    {
        if (sign < 0 && m_currentScaleFactorIndex > 0)
        {
            m_currentScaleFactorIndex--;
        }
        else if (sign > 0 && m_currentScaleFactorIndex < m_numberOfScalingFactors - 1)
        {
            m_currentScaleFactorIndex++;
        }
    }
}
