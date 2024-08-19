using UnityEngine;
using UnityEngine.Events;

public class WinGameListener : MonoBehaviour
{
    [SerializeField] private GameState so_gameState;
    [Tooltip("Use this event to trigger feature of the Unity Engine, like particles.")]
    /*
    *NOTE:
    For each new event from the SO state, it may be necesary a new UnityEvent as individual response.
    */
    public UnityEvent unityResponseToWinGame;
    private void Awake()
    {
        try
        {
            if(!so_gameState)
            throw new System.NullReferenceException();
        }
        catch
        {
            Debug.Log("Make sure to add the GameState");
        }
    }
    public void OnWinGame()
    {
        unityResponseToWinGame.Invoke();
    }

    private void OnEnable()
    {
        so_gameState.RegisterListener(this);
    }
    private void OnDisable()
    {
        so_gameState.UnregisterListener(this);
    }
}