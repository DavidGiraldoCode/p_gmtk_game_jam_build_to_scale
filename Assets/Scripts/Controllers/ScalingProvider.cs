using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingProvider : MonoBehaviour
{
    [SerializeField] private PlayerState so_playerState;
    public void ShootScalingRay(float sign, Vector2 screenPoint)
    {
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
    }
}
