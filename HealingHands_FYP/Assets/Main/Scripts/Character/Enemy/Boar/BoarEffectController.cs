using UnityEngine;

public class BoarEffectController : EffectController
{
    [SerializeField] private TransformAnchor _protagonist;
    private Boar _boar;

    public ParticleSystem WhiffParticle;
    public ParticleSystem DizzyParticle;
    public ParticleSystem RushParticle;

    private Vector2 _direction;
    private float _angle;

    public void PlayBrushParticle() => WhiffParticle.Play();
    public void PlayDizzyParticle() => DizzyParticle.Play();

    private void Start()
    {
        _boar =GetComponent<Boar>();
    }

    private void Update()
    {
        _direction = (_protagonist.Value.position - transform.position).normalized;
        _angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
    
        if(_boar.IsPreparingAttack)
        SetWhiffParticleOrigin();

        if (!_boar.IsAttacking)
        SetRushParticle();
    }

    private void SetWhiffParticleOrigin()
    {
        if (45 >= _angle && _angle >= -45)
        {
            WhiffParticle.transform.position = (Vector2)transform.position + new Vector2(0, 0.8f);
            WhiffParticle.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (135 >= _angle && _angle >= 45)
        {
            WhiffParticle.transform.position = (Vector2)transform.position + new Vector2(0.7f, -0.4f);
            WhiffParticle.transform.rotation = Quaternion.Euler(0, 0, -135);
        }
        else if (-135 <= _angle && _angle <= -45)
        {
            WhiffParticle.transform.position = (Vector2)transform.position + new Vector2(-0.7f, -0.4f);
            WhiffParticle.transform.rotation = Quaternion.Euler(0, 0, 135);
        }   
        else
        {
            WhiffParticle.transform.position = (Vector2)transform.position - new Vector2(0, 0.8f);
        }
    }

    private void SetRushParticle()
    {
        if (45 >= _angle && _angle >= -45)
        {
            RushParticle.transform.position = (Vector2)transform.position - new Vector2(0, -0.5f);
            RushParticle.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (135 >= _angle && _angle >= 45)
        {
            RushParticle.transform.position = (Vector2)transform.position + new Vector2(-0.6f, 0.45f);
            RushParticle.transform.rotation = Quaternion.Euler(0, 0, -60);
        }
        else if (-135 <= _angle && _angle <= -45)
        {
            RushParticle.transform.position = (Vector2)transform.position + new Vector2(0.6f, 0.45f);
            RushParticle.transform.rotation = Quaternion.Euler(0, 0, 60);
        }
        else
        {
            RushParticle.transform.position = (Vector2)transform.position + new Vector2(0, 0.5f);
        }
    }
}
