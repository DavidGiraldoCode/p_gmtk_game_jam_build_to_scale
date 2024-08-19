using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance { get; private set; }
    [SerializeField] private Canvas m_startGUI;
    [SerializeField] private Canvas m_loseGUI;
    [SerializeField] private Canvas m_winGUI;

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

        SettingUpAvailableGUIs();
    }
    public void SettingUpAvailableGUIs()
    {
        if (m_startGUI)
            m_startGUI.enabled = true;
        if (m_loseGUI)
            m_loseGUI.enabled = false;
        if (m_winGUI)
            m_winGUI.enabled = false;
    }

    public void GUIReactionToStartGame()
    {
        if (m_startGUI)
            m_startGUI.enabled = false;
        if (m_loseGUI)
            m_loseGUI.enabled = false;
        if (m_winGUI)
            m_winGUI.enabled = false;
    }
    public void GUIReactionToLoseGame()
    {
        if (m_loseGUI)
            m_loseGUI.enabled = true;
    }
    public void GUIReactionToWinGame()
    {
        if (m_winGUI)
            m_winGUI.enabled = true;
    }

}
