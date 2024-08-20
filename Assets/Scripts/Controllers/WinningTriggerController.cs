using UnityEngine;
[RequireComponent(typeof(CapsuleCollider))]
public class WinningTriggerController : MonoBehaviour
{
    [SerializeField] private GameState so_gameState;
     private CapsuleCollider m_capColl;
    private void Awake()
    {
        m_capColl = GetComponent<CapsuleCollider>();
        m_capColl.isTrigger = true;
        m_capColl.height = 2.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<FirstPersonController>(out var fpc)) return;
        so_gameState.WinGame();
    }
}
