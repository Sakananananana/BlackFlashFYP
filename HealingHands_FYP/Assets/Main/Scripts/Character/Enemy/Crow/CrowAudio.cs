using UnityEngine;

public class CrowAudio : MonoBehaviour
{
    //Move To Audio Script Later
    [SerializeField] private AudioChannelSO _audioChannelSO;
    [SerializeField] private AudioConfiguration _audioConfig;

    [SerializeField] private AudioData _crowFlapAudio;
    [SerializeField] private AudioData _crowAttackAudio;

    public void PlayCrowFlapAudio() => _audioChannelSO.OnAudioPlayRequested(_crowFlapAudio, _audioConfig);
    public void PlayCrowAttackAudio() => _audioChannelSO.OnAudioPlayRequested(_crowAttackAudio, _audioConfig);
}
