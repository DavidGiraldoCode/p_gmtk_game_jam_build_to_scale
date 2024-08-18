using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingProvider : MonoBehaviour
{
    [SerializeField] private PlayerState so_playerState;
    [SerializeField] private RayTracerController m_rayTracerController;
    private float m_cooldownTime = 5.0f;
    private float m_cooldownCounter = 5.0f;
    private float m_cooldownSpeed = 10f;
    private bool m_readyToShoot = true;

    private void FixedUpdate()
    {
        if (!m_readyToShoot)
            m_cooldownCounter -= m_cooldownSpeed * Time.fixedDeltaTime;

        if (m_cooldownCounter <= 0)
        {
            m_cooldownCounter = m_cooldownTime;
            m_readyToShoot = true;
        }

    }
    public void ShootScalingRay(float sign, Vector2 screenPoint)
    {
        if (!m_readyToShoot) return;
        m_rayTracerController.ShootRayBeam(sign);
        
        //Debug.Log("Shooting");
        //Debug.Log(sign);

        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject geometry = hit.collider.gameObject;
            ScalingController scaler;
            if (!geometry.TryGetComponent<ScalingController>(out scaler)) return;
            float scaleFactor = 1.0f + (so_playerState.CurrentScaleFactor * sign);

            scaler.TriggerScaling(scaleFactor);

        }
        m_readyToShoot = false;
    }
}
