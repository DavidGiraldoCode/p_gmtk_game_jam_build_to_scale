using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class LosingTriggerController : MonoBehaviour
{
    [SerializeField] private GameState so_gameState;
    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<FirstPersonController>(out var fpc)) return;
        so_gameState.LoseGame();
    }
}
