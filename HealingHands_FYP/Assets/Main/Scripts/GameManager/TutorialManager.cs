using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    private const string TutorialCompletedKey = "TutorialCompleted";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        // Reset on key combination (e.g., Ctrl + R)
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs have been reset!");
        }
    }

    public void OnTutorialComplete()
    {
        // Mark the tutorial as completed in PlayerPrefs
        PlayerPrefs.SetInt(TutorialCompletedKey, 1);
        PlayerPrefs.Save(); // Ensure it is saved
    }

    public bool IsTutorialCompleted()
    {
        // Check if the tutorial is marked as completed
        return PlayerPrefs.GetInt(TutorialCompletedKey, 0) == 1;     
    }

}
