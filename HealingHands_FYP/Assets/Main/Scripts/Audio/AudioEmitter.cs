using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioEmitter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    public event UnityAction<AudioEmitter> OnSoundFinishedPlaying;

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
    }

    public void Play(AudioData data, AudioConfiguration audioCofig, Vector3 position)
    {
        audioCofig.ApplyTo(_audioSource);

        _audioSource.transform.position = position;
        _audioSource.clip = data.GetAudioClip();
        _audioSource.loop = data.ApplyLoop;

        //Remember to Check how this affects the Looping BGM
        if (!_audioSource.loop)
        {
            //if the requested audio is playing stop the coroutine and reset it
            if (_playAudioCoroutine != null)
            {
                StopCoroutine(_playAudioCoroutine);
            }

            if (data.ApplyPitchChange)
            { _audioSource.pitch += Random.Range(-0.05f, 0.05f); }

            _audioSource.Play();
            _playAudioCoroutine = StartCoroutine(WaitForAudioEnds(_audioSource.clip.length));
        }
        else
        {
            _audioSource.Play();
        }
    }

    public bool IsLooping()
    {
        return _audioSource.loop;
    }

    private IEnumerator WaitForAudioEnds(float audioLength)
    {
        yield return new WaitForSeconds(audioLength);
        NotifyBeingDone();
    }

    private void NotifyBeingDone()
    {
        OnSoundFinishedPlaying.Invoke(this);
    }
}
