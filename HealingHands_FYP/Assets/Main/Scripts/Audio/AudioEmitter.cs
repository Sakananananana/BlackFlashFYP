using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioEmitter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private Coroutine _playAudioCoroutine;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Stop()
    {
        if (_playAudioCoroutine != null)
        {
            StopCoroutine(_playAudioCoroutine);
            _playAudioCoroutine = null;
        }

        _audioSource.Stop();
        AudioManager.Instance.ReturnToPool(this);
    }

    public void Play(AudioData data, AudioConfiguration audioCofig, Vector3 position)
    {
        audioCofig.ApplyTo(_audioSource);

        _audioSource.transform.position = position;
        _audioSource.clip = data.GetAudioClip();
        _audioSource.loop = data.ApplyLoop;

        _audioSource.Play();
        if (!_audioSource.loop)
        {
            _playAudioCoroutine = StartCoroutine(WaitForAudioEnds(_audioSource.clip.length));
        }
    }

    private IEnumerator WaitForAudioEnds(float audioLength)
    {
        yield return new WaitForSeconds(audioLength);
        AudioManager.Instance.ReturnToPool(this);
    }
}
