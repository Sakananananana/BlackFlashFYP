using UnityEngine;

public class Projectile : Attack
{
    [SerializeField] AudioConfiguration _audioConfig;
    [SerializeField] AudioChannelSO _audioChannelSO;
    [SerializeField] AudioData _hitAudioCue;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (!other.CompareTag(gameObject.tag))
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                PlayHitProjectileAudio();
                Destroy(gameObject);
            } 
        }

        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }

    private void PlayHitProjectileAudio() => _audioChannelSO.OnAudioPlayRequested(_hitAudioCue, _audioConfig);
}
