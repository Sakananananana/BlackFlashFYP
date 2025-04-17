using UnityEngine;

public class BoarEffectController : EffectController
{
    public ParticleSystem BrushParticle;

    public void PlayBrushParticle() => DeathParticle.Play();
}
