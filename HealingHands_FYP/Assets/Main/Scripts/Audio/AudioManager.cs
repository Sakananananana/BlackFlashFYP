using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioChannelSO _audioChannelSO;
    [SerializeField] private AudioEmitterPoolSO _audioEmitterPool = default;

    private AudioEmitter _audioEmitter;

    [SerializeField] private AudioMixer _audioMixer;

    private int _initNum = 10;


    private void Awake()
    {
        DontDestroyOnLoad(this);

        _audioEmitterPool.Prewarm(_initNum);
        _audioEmitterPool.SetParent(this.transform);
    }

    private void OnEnable()
    {
        _audioChannelSO.OnAudioPlayRequested += PlayRaisedAudio;
    }

    private void OnDestroy()
    {
        _audioChannelSO.OnAudioPlayRequested -= PlayRaisedAudio;
    }

    private void PlayRaisedAudio(AudioData audioData, AudioConfiguration audioConfig, Vector3 position = default)
    {
        _audioEmitter = _audioEmitterPool.Request();
        _audioEmitter.Play(audioData, audioConfig, position);

        if (!audioData.ApplyLoop)
            _audioEmitter.OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
    }

    private void OnSoundEmitterFinishedPlaying(AudioEmitter emitter)
    {
        StopAndCleanEmitter(emitter);
    }

    private void StopAndCleanEmitter(AudioEmitter emitter)
    {
        if (!emitter.IsLooping())
            emitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;

        emitter.Stop();
        _audioEmitterPool.Return(emitter);
    }

}
