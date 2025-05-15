using UnityEngine;

public class SetUIActive : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject uiPanel; // Assign the UI panel in the Inspector
    public string playerTag = "Player"; // Tag your player as "Player"

    private void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false); // Hide initially
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (uiPanel != null)
                uiPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (uiPanel != null)
                uiPanel.SetActive(false);
        }
    }
}
