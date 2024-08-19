using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

[CreateAssetMenu(fileName = "SceneRef", menuName = "SceneRef", order = 0)]
public class SceneRef : ScriptableObject 
{
    public string path;
}
