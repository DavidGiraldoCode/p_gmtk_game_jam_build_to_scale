using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingTriggerController : MonoBehaviour
{
    [SerializeField] private GameState so_gameState;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.TryGetComponent<FirstPersonController>(out var fpc)) return;
        so_gameState.LoseGame();
    }
}
