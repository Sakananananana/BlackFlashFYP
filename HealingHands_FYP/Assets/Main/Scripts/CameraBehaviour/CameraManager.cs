using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Impulse Settings")]
    [SerializeField] private float _shakeForce;
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    [SerializeField] private CinemachineConfiner2D _confiner2D;

    [Header("Listening to...")]
    [SerializeField] private VoidEventChannelSO _camShakeEvent = default;
    [SerializeField] private ColliderEventChannelSO _repositionCam;

    private void OnEnable()
    {
        _camShakeEvent.OnEventRaised += GenerateCamImpulseWithPattern;
        _repositionCam.OnEventRaised += PositionCameraToRoom;
    }

    private void OnDisable()
    {
        _camShakeEvent.OnEventRaised -= GenerateCamImpulseWithPattern;
        _repositionCam.OnEventRaised -= PositionCameraToRoom;
    }

    public void GenerateCamImpulseWithPattern()
    {
        //_impulseSource.GenerateImpulseWithVelocity(); Need to Pass in player's Attack Direction
        _impulseSource.GenerateImpulseWithForce(_shakeForce);
    }

    private void PositionCameraToRoom(BoxCollider2D col)
    {
        _confiner2D.BoundingShape2D = col;
        _confiner2D.InvalidateBoundingShapeCache();
    }
}
