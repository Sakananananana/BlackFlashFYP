using UnityEngine;

public class BoarAudio : MonoBehaviour
{
    //Move To Audio Script Later
    [SerializeField] private AudioChannelSO _audioChannelSO;
    [SerializeField] private AudioConfiguration _audioConfig;

    [SerializeField] private AudioData _boarDizzyAudio;
    [SerializeField] private AudioData _boarSnortAudio;
    [SerializeField] private AudioData _boarDeadAudio;
    [SerializeField] private AudioData _boarAttackAudio;

    public void PlayBoarDizzyAudio() => _audioChannelSO.OnAudioPlayRequested(_boarDizzyAudio, _audioConfig);
    public void PlayBoarSnortAudio() => _audioChannelSO.OnAudioPlayRequested(_boarSnortAudio, _audioConfig);
    public void PlayBoarDeadAudio() => _audioChannelSO.OnAudioPlayRequested(_boarDeadAudio, _audioConfig);
    public void PlayBoarSmashAudio() => _audioChannelSO.OnAudioPlayRequested(_boarAttackAudio, _audioConfig);
}
