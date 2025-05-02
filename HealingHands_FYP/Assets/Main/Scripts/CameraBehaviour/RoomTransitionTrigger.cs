using UnityEngine;
using System.Collections;
using PlayerInputSystem;

public class RoomTransitionTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform newPos;
    [SerializeField] private ColliderEventChannelSO colliderEventChannel;
    [SerializeField] private InputReader inputReader; // your InputReader script (e.g., PlayerInputReader)
    //[SerializeField] private TriggerZoneManager zoneManager;


    [Header("Transition Targets")]
    [SerializeField] private Vector3 playerTargetPosition;
    [SerializeField] private BoxCollider2D newCameraBounds;

    [Header("Settings")]
    [SerializeField] private float transitionDelay = 0.2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        //zoneManager.ActivateTrigger(this.gameObject);
        StartCoroutine(TransitionRoom(other));
    }

    private IEnumerator TransitionRoom(Collider2D other)
    {
        // Disable all player input via InputReader
        inputReader.DisableAllInput();

        // Move player to new room (use Rigidbody2D for correct physics behavior)
        other.transform.position = newPos.position;
        //Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        //rb.position = new Vector2(playerTargetPosition.x, playerTargetPosition.y + 5);
        //rb.linearVelocity = Vector2.zero; // Optional: stop existing movement

            

        // Update the camera bounds for the new room
        colliderEventChannel.RaiseEvent(newCameraBounds);

        // Optional short delay to prevent retriggers
        yield return new WaitForSeconds(transitionDelay);

        // Re-enable gameplay input
        inputReader.SetGameplay();
    }
}

