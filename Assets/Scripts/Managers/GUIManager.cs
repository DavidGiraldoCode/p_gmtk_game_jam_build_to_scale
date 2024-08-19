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

        m_startGUI.enabled = true;
        m_loseGUI.enabled = false;
        m_winGUI.enabled = false;
    }

    public void GUIReactionToStartGame()
    {
        m_startGUI.enabled = false;
    }

}
