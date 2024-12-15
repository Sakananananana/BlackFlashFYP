using UnityEngine;
using Unity.Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager CameraShakeInstance;

    [SerializeField] private float _shakeForce;

    private void Awake()
    {
        if (CameraShakeInstance != null && CameraShakeInstance != this)
        {
            Destroy(this);
        }
        else
        {
            CameraShakeInstance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(_shakeForce);
    }
}
