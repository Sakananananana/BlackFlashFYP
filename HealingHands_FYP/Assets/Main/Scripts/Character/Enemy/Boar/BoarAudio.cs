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
}
