using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioChannelSO _audioChannelSO;
    [SerializeField] private AudioData _audioData;
    [SerializeField] private AudioConfiguration _audioConfig;

    private void Start()
    {
        _audioChannelSO.OnAudioPlayRequested(_audioData, _audioConfig);
    }
}
