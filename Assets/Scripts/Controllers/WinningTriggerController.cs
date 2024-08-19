using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class WinningTriggerController : MonoBehaviour
{
    [SerializeField] private GameState so_gameState;
    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;   
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(!other.gameObject.TryGetComponent<FirstPersonController>(out var fpc)) return;
        so_gameState.WinGame();
    }
}
