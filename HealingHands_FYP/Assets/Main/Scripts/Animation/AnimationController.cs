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
        ////If _direction's Vec2 is equal to zero this will not be called!
        //if (_direction != Vector2.zero)
        //{
        //    if (_direction.y > 0 || (_direction.y > 0 && Mathf.Abs(_direction.x) > 0))
        //    { _dirUp = 1; }
        //    else
        //    { _dirUp = 0; }

        //    if (_direction.y < 0 || (_direction.y < 0 && Mathf.Abs(_direction.x) > 0))
        //    { _dirDown = 1; }
        //    else
        //    { _dirDown = 0; }

        //    if (_direction.x > 0.8)
        //    { _dirRight = 1; }
        //    else
        //    { _dirRight = 0; }

        //    if (_direction.x < -0.8)
        //    { _dirLeft = 1; }
        //    else
        //    { _dirLeft = 0; }

        //    _anim.SetFloat("Up", _dirUp);
        //    _anim.SetFloat("Down", _dirDown);
        //    _anim.SetFloat("Left", _dirLeft);
        //    _anim.SetFloat("Right", _dirRight);
        //}
    }
}
