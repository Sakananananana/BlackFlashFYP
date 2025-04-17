using UnityEngine;

public class AnimationController : MonoBehaviour
{
    //Directions
    protected Vector2 _direction;

    protected float _dirUp;
    protected float _dirDown;
    protected float _dirLeft;
    protected float _dirRight;

    //Animator Reference
    protected Animator _anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        _anim = GetComponent<Animator>();
        _anim.SetFloat("Down", 1);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       
    }
}
