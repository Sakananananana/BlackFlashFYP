using UnityEngine;

public class TigerAudio : MonoBehaviour
{
    [SerializeField] private AudioChannelSO _audioChannelSO;
    [SerializeField] AudioConfiguration _audioConfig;

    [SerializeField] private AudioData _tigerRoarAudio;
    [SerializeField] private AudioData _tigerBiteAudio;

    public void PlayRoarAudio() => _audioChannelSO.OnAudioPlayRequested(_tigerRoarAudio, _audioConfig);
    public void PlayBiteAudio() => _audioChannelSO.OnAudioPlayRequested(_tigerBiteAudio, _audioConfig);
}
