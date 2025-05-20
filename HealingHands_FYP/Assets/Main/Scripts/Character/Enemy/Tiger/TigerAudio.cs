using UnityEngine;

public class TigerAudio : MonoBehaviour
{
    [SerializeField] private AudioChannelSO _audioChannelSO;
    [SerializeField] AudioConfiguration _audioConfig;

    [SerializeField] private AudioData _tigerRoarAudio;

    public void PlayRoarAudio() => _audioChannelSO.OnAudioPlayRequested(_tigerRoarAudio, _audioConfig);
}
