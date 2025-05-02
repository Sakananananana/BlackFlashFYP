using UnityEngine;

public class TriggerZoneManager : MonoBehaviour
{
    [SerializeField] public GameObject triggerA;
    [SerializeField] public GameObject triggerB;
    [SerializeField] public GameObject triggerC;
    [SerializeField] public GameObject triggerD;

    public void ActivateTrigger(GameObject triggered)
    {
        if (triggered == triggerA)
        {
            triggerB.SetActive(false);
            //triggerA.SetActive(true);
        }
        else if (triggered == triggerB)
        {
            triggerA.SetActive(false);
            //triggerB.SetActive(true);
        }
        else if (triggered == triggerC)
        {
            triggerB.SetActive(true);
            //triggerB.SetActive(true);
        }
        else if (triggered == triggerD)
        {
            triggerA.SetActive(true);
            //triggerB.SetActive(true);
        }
    }
}
