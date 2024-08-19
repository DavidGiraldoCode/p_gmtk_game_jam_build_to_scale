using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "GameState", order = 0)]
public class GameState : ScriptableObject
{
    [SerializeField] private bool m_win = false;
    public bool Win { get => m_win; set => m_win = value; }
    [SerializeField] private bool m_lose = false;
    public bool Lose { get => m_lose; set => m_lose = value; }
    private bool m_gameHasStarted = false;
    public bool GameHasStarted { get => m_gameHasStarted; }
    [SerializeField] private bool m_currentLevelCompleted = false;
    public bool CurrentLevelCompleted { get => m_currentLevelCompleted;}

    //* ========================================================================================= EVENTS
    private List<GameStartListener> m_gameStartListeners = new List<GameStartListener>();
    public void StartGame()
    {
        m_gameHasStarted = true;
        m_win = false;
        m_lose = false;
        m_currentLevelCompleted = false;
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        for (int i = m_gameStartListeners.Count - 1; i >= 0; i--)
            m_gameStartListeners[i].OnStartGame();
    }
    public void RegisterListener(GameStartListener listener)
    {
        if (!m_gameStartListeners.Contains(listener))
            m_gameStartListeners.Add(listener);
    }
    public void UnregisterListener(GameStartListener listener)
    {
        if (m_gameStartListeners.Contains(listener))
            m_gameStartListeners.Remove(listener);
    }
    //* ========================================================================================= EVENTS
    //private List<WinGameListener> m_winGameListeners = new List<WinGameListener>();
    public void WinGame()
    {
        m_win = true;
        m_gameHasStarted = false;
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
        // for (int i = m_winGameListeners.Count - 1; i >= 0; i--)
        //     m_winGameListeners[i].OnWinGame();
    }
    // public void RegisterListener(WinGameListener listener)
    // {
    //     if (!m_winGameListeners.Contains(listener))
    //         m_winGameListeners.Add(listener);
    // }
    // public void UnregisterListener(WinGameListener listener)
    // {
    //     if (m_winGameListeners.Contains(listener))
    //         m_winGameListeners.Remove(listener);
    // }
    //* ========================================================================================= EVENTS
    private List<LoseGameListener> m_loseGameListeners = new List<LoseGameListener>();
    public void LoseGame()
    {
        m_gameHasStarted = false;
        m_lose = true;

        for (int i = m_loseGameListeners.Count - 1; i >= 0; i--)
            m_loseGameListeners[i].OnLoseGame();
    }
    public void RegisterListener(LoseGameListener listener)
    {
        if (!m_loseGameListeners.Contains(listener))
            m_loseGameListeners.Add(listener);
    }
    public void UnregisterListener(LoseGameListener listener)
    {
        if (m_loseGameListeners.Contains(listener))
            m_loseGameListeners.Remove(listener);
    }

    public void ProgessToNextLevel()
    {
        m_currentLevelCompleted = false;
    }

}