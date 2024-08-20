using UnityEngine;
[RequireComponent(typeof(CapsuleCollider))]
public class NextSceneTriggerController : MonoBehaviour
{
    private CapsuleCollider m_capColl;
    private void Awake()
    {
        m_capColl = GetComponent<CapsuleCollider>();
        m_capColl.isTrigger = true;
        m_capColl.height = 2.0f;
    }
    private void Start()
    {
        if (!GamePlayManager.Instance)
            throw new System.NullReferenceException("The NextSceneTriggerController needs the GamePlayManager to load the next scene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<FirstPersonController>(out var fpc)) return;
        if (GamePlayManager.Instance)
            GamePlayManager.Instance.LoadNextScene();
    }
}
