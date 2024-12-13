using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class AreaTransition : MonoBehaviour
{
    
    [SerializeField] private PolygonCollider2D _mapBoundary;
    private CinemachineConfiner _cineConfiner;

    //private CameraController cam;
    //private CinemachineVirtualCameraBase _vCamBase;

    //public Vector2 newMinPos;
    //public Vector2 newMaxPos;
    public Vector3 movePlayer;
    
    void Awake()
    {
        if (_cineConfiner == null)
            _cineConfiner = FindAnyObjectByType<CinemachineConfiner>();

        //if (_vCamBase == null)
        //    _vCamBase = FindAnyObjectByType<CinemachineVirtualCameraBase>();
        //cam = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            
            _cineConfiner.m_BoundingShape2D = _mapBoundary;
            
            other.transform.position += movePlayer;
        }
    }
}
