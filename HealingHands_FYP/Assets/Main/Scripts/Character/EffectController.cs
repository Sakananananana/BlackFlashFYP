using UnityEngine;

public class EffectController : MonoBehaviour
{
    public ParticleSystem DeathParticle;

    public void PlayDeathParticle() => DeathParticle.Play();
}
