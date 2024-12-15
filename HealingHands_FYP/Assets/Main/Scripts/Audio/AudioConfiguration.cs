using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "new AudioConfiguration", menuName = "Scriptable Objects/Audio/AudioConfiguration")]
public class AudioConfiguration : ScriptableObject
{
    public AudioMixerGroup OutputAudioMixerGroup;

    [Range(0f, 1f)] public float SpatialBlend;

    public void ApplyTo(AudioSource audioSource)
    {
        audioSource.outputAudioMixerGroup = this.OutputAudioMixerGroup;
        audioSource.spatialBlend = this.SpatialBlend;
    }
}
