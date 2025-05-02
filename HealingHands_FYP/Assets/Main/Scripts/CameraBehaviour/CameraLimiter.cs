using UnityEngine;

public class CameraLimiter : MonoBehaviour
{
    [SerializeField] private ColliderEventChannelSO colliderEventChannel;
    [SerializeField] private BoxCollider2D boundsCollider;
    [SerializeField] private GameObject trigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            colliderEventChannel.RaiseEvent(boundsCollider);
        }

    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    trigger.SetActive(true);
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    trigger.SetActive(false);
    //}
}


