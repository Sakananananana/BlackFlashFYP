using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects /Audio /AudioData")]
public class AudioData : ScriptableObject
{
    [Header("Audio Playing Mode")]
    public PlayMode ChoosePlayMode;

    [Header("Audio Properties")]
    public bool ApplyLoop = false;
    public bool ApplyPitchChange = false;

    [Header("Audio Album")]
    public AudioClip[] AudioClips;
    private int _nextClipToPlay = -1;

    public AudioClip GetAudioClip()
    {
        if (AudioClips.Length == 1)
        {
            return AudioClips[0];
        }
        else
        {
            switch (ChoosePlayMode)
            {
                case PlayMode.Sequential:
                    {
                        _nextClipToPlay++;
                        _nextClipToPlay = (_nextClipToPlay < AudioClips.Length) ? _nextClipToPlay : 0;
                    }
                    break;

                case PlayMode.Random:
                    {
                        _nextClipToPlay = UnityEngine.Random.Range(0, AudioClips.Length);
                    }
                    break;
            }

            return AudioClips[_nextClipToPlay];
        }
    }

    public enum PlayMode
    {
        Sequential, Random
    }
}
