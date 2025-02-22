using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Impulse Settings")]
    [SerializeField] private float _shakeForce;
    [SerializeField] private CinemachineImpulseSource _impulseSource;

    [Header("Listening to...")]
    [SerializeField] private VoidEventChannelSO _camShakeEvent = default;

    private void OnEnable()
    {
        _camShakeEvent.OnEventRaised += GenerateCamImpulseWithPattern;
    }

    private void OnDisable()
    {
        _camShakeEvent.OnEventRaised -= GenerateCamImpulseWithPattern;
    }

    public void GenerateCamImpulseWithPattern()
    {
        //_impulseSource.GenerateImpulseWithVelocity(); Need to Pass in player's Attack Direction
        _impulseSource.GenerateImpulseWithForce(_shakeForce);
    }
}
