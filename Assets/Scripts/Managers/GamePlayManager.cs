using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance { get; private set; }
    [SerializeField] private GameState so_gameState;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        if (!so_gameState)
            throw new System.NullReferenceException("The GameState Scriptable Object is missing on the GamePlayManager");

        so_gameState.ResetGameState();
    }
}
