using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioData _audioData;
    [SerializeField] private AudioConfiguration _audioConfig;

    private void Start()
    {
        AudioManager.Instance.PlayRaisedAudio(_audioData, _audioConfig);
    }
}
