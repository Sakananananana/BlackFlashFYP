using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaTransition : MonoBehaviour
{
    private CameraController cam;

    public Vector2 newMinPos;
    public Vector2 newMaxPos;
    public Vector3 movePlayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            Debug.Log("enter");
            cam.minPosition = newMinPos;
            cam.maxPosition = newMaxPos;
        }
    }
}
