using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class AudioManager : MonoBehaviour
{
    //Singleton Declaration
    public static AudioManager Instance { get; private set; }

    //Object Pool
    private IObjectPool<AudioEmitter> _audioEmitterPool;

    //The Audio Emitter Prefab
    [SerializeField] private AudioEmitter _audioEmitterPrefab;

    //Keep Track of Currently Active Pool
    //Prolly can switch to use the currently inactive (_audioEmitterPool.InactiveCount)
    private readonly List<AudioEmitter> _activeAudioEmitterPool = new();

    [SerializeField] private AudioMixer _audioMixer;

    private bool _collectionCheck;
    public int _defaultCapacity = 10;
    public int _maxPoolSize = 15;


    //To My Understanding,
    //this is to check whether if the same sound has created to the max and only can be this much
    //which i think this can be handled by the _maxpoolsize
    //private int _maxSoundInstances = 30;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _audioEmitterPool =
            new ObjectPool<AudioEmitter>(CreateSoundEmitter,
            OnTakeFromPool, OnReturnPool, OnDestroyPoolObject,
            _collectionCheck, _defaultCapacity, _maxPoolSize);
    }

    #region Sound Emitter Pool Functions
    AudioEmitter CreateSoundEmitter()
    {
        AudioEmitter audioEmitter = Instantiate(_audioEmitterPrefab);
        audioEmitter.transform.SetParent(transform);
        audioEmitter.gameObject.SetActive(false);

        return audioEmitter;
    }

    //These are the procedual that gonna take
    //when a sound emitter is "get" from the pool
    //or is "return" to pool
    void OnTakeFromPool(AudioEmitter audioEmitter)
    {
        audioEmitter.gameObject.SetActive(true);
        _activeAudioEmitterPool.Add(audioEmitter);
    }

    void OnReturnPool(AudioEmitter audioEmitter)
    {
        audioEmitter.gameObject.SetActive(false);
        _activeAudioEmitterPool.Remove(audioEmitter);
    }

    //Destroy Excessive Objects in Pool
    void OnDestroyPoolObject(AudioEmitter audioEmitter)
    {
        Destroy(audioEmitter.gameObject);
    }
    #endregion

    #region Call to Get or Release Sound Emitters
    //By Getting it will be automatically set to active
    public AudioEmitter Get()
    {
        return _audioEmitterPool.Get();
    }

    //Call to Perform Releasing Action of the Object To Pool
    public void ReturnToPool(AudioEmitter audioEmitter)
    {
        //When I call this ReturnToPool Function, it will activate OnReturnPool?
        _audioEmitterPool.Release(audioEmitter);
    }
    #endregion


    //Need To Be Extract out into another script called channel
    public void PlayRaisedAudio(AudioData audioData, AudioConfiguration audioConfig, Vector3 position = default)
    {
        AudioEmitter audioEmitter = Get();
        audioEmitter.Play(audioData, audioConfig, position);
    }

    //check if there is any inactive sound emitters
}
