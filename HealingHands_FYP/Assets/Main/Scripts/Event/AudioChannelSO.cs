using UnityEngine;

[CreateAssetMenu(fileName = "AudioChannelSO", menuName = "Scriptable Objects/AudioChannelSO")]
public class AudioChannelSO : ScriptableObject
{
    public AudioPlayAction OnAudioPlayRequested;

    public void RaisePlayEvent(AudioData audioData, AudioConfiguration audioConfig, Vector3 position = default)
    {
        OnAudioPlayRequested?.Invoke(audioData, audioConfig, position) ;
    }

    public delegate void AudioPlayAction(AudioData audioData, AudioConfiguration audioConfig, Vector3 position = default);
}
