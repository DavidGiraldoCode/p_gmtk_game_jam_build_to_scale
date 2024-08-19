using System.Collections;
using UnityEditor.SearchService;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance { get; private set; }
    [SerializeField] private GameState so_gameState;
    [SerializeField] private SceneRef so_sceneToLoadNext;
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

        if (!so_sceneToLoadNext)
            throw new System.NullReferenceException("The GamePlayManager always needs a SceneRef asset to load the next scene");

        //TODO
        Debug.Log(SceneManager.GetActiveScene().name);

    }

    private void Update()
    {


    }

    public void LoadNextScene()
    {

        SceneManager.LoadScene(so_sceneToLoadNext.path);
    }

    //TODO
    float counter = 0F;
    bool stop = false;
    public void TestingSceneLoading()
    {
        counter += Time.deltaTime;
        if (counter > 2.0f && !stop)
        {
            stop = true;
            Debug.Log("Load scene");
            SceneManager.LoadScene(so_sceneToLoadNext.path);
        }
    }
}
