using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class NextSceneTriggerController : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
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
