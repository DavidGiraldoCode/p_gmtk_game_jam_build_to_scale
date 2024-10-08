using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingProvider : MonoBehaviour
{
    [SerializeField] private PlayerState so_playerState;
    [SerializeField] private RayBeamProvider m_rayBeamProvider;
    private float m_cooldownCounter;
    private const float ONE_SECOND = 1.0f;

    private void Start()
    {
        m_cooldownCounter = so_playerState.CoolDown;
    }
    private void FixedUpdate()
    {
        if (!so_playerState.ReadyToShoot)
        {
            m_cooldownCounter -= ONE_SECOND * Time.fixedDeltaTime;
        }


        if (m_cooldownCounter <= 0)
        {
            m_cooldownCounter = so_playerState.CoolDown;
            so_playerState.ReadyToShoot = true;
        }

        //Debug.Log(m_cooldownCounter);
    }
    public void ShootScalingRay(float sign, Vector2 screenPoint)
    {
        if (!so_playerState.ReadyToShoot) return;
        so_playerState.ReadyToShoot = false;
        //? here they ray show even if nothing is hit m_rayTracerController.ShootRayBeam(sign);

        //Debug.Log("Shooting");
        //Debug.Log(sign);

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            m_rayBeamProvider.ShootRayBeam(sign, ray, hit.point); //Only when the ray hit something with at least a Collider

            GameObject geometry = hit.collider.gameObject;
            ScalingController scaler;
            if (!geometry.TryGetComponent<ScalingController>(out scaler)) return;
            float scaleFactor = 1.0f + (so_playerState.CurrentScaleFactor * sign);

            scaler.TriggerScaling(scaleFactor);
        }

    }
}
