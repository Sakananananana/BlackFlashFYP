using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Impulse Settings")]
    [SerializeField] private float _shakeForce;
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    [SerializeField] private CinemachineConfiner2D _confiner2D;
    [SerializeField] private CinemachinePixelPerfect _ppCam;
    [SerializeField] private CinemachineCamera _vCam;


    [Header("Listening to...")]
    [SerializeField] private VoidEventChannelSO _camShakeEvent = default;
    [SerializeField] private ColliderEventChannelSO _repositionCam;
    [SerializeField] private TransformEventChannelSO _onSetCameraPos;
    [SerializeField] private VoidEventChannelSO _playerDeathEvent;


    private void OnEnable()
    {
        _onSetCameraPos.OnEventRaised += SetCameraPosition;
        _camShakeEvent.OnEventRaised += GenerateCamImpulseWithPattern;
        _repositionCam.OnEventRaised += PositionCameraToRoom;
        _playerDeathEvent.OnEventRaised += DeathEvent;
    }

    private void OnDisable()
    {
        _onSetCameraPos.OnEventRaised -= SetCameraPosition;
        _camShakeEvent.OnEventRaised -= GenerateCamImpulseWithPattern;
        _repositionCam.OnEventRaised -= PositionCameraToRoom;
        _playerDeathEvent.OnEventRaised -= DeathEvent;
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

    private void SetCameraPosition(Transform target)
    {
        _vCam.Target.TrackingTarget = target;
    }

    private void DeathEvent()
    {
        StartCoroutine(CameraZooomIn());
    }

    private IEnumerator CameraZooomIn()
    {
        _ppCam.enabled = false;

        float initialSize = _vCam.Lens.OrthographicSize;
        float targetZoom = _vCam.Lens.OrthographicSize - 3;
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            _vCam.Lens.OrthographicSize = Mathf.SmoothStep(initialSize, targetZoom, elapsed / 1);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _ppCam.enabled = true;
        _vCam.Lens.OrthographicSize = targetZoom;
    }
}
