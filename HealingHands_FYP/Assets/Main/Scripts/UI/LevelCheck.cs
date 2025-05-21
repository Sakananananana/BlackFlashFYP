using UnityEngine;

public class LevelCheck : MonoBehaviour
{
    public GameObject lockCollider;     // The lock visual or blocker
    public GameObject uiPanel;          // The UI shown when player enters
    public string playerTag = "Player"; // Tag used by player GameObject

    private bool isUnlocked;

    void Start()
    {
        CheckUnlockStatus();
        if (uiPanel != null)
            uiPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && isUnlocked)
        {
            uiPanel?.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && isUnlocked)
        {
            uiPanel?.SetActive(false);
        }
    }

    // Call this when player defeats the boss
    public void CheckUnlockStatus()
    {
        isUnlocked = PlayerPrefs.GetInt("Level1BossDefeated", 0) == 1;
        Debug.Log("Level1BossDefeated = " + PlayerPrefs.GetInt("Level1BossDefeated", 0));

        if (lockCollider != null)
            lockCollider.SetActive(!isUnlocked);
    }
}
