using System;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioChannelSO _audioChannelSO;

    [SerializeField] AudioData _dashAudio;
    [SerializeField] AudioData _slashAudio;
    [SerializeField] AudioData _footstepAudio;
    [SerializeField] AudioData _inventoryAudio;

    [SerializeField] AudioConfiguration _audioConfig;

    public void PlayInventoryAudio() => _audioChannelSO.OnAudioPlayRequested(_inventoryAudio, _audioConfig);
    public void PlayFootstepAudio() => _audioChannelSO.OnAudioPlayRequested(_footstepAudio, _audioConfig);
    public void PlaySlashAudio() => _audioChannelSO.OnAudioPlayRequested(_slashAudio, _audioConfig);
    public void PlayDashAudio() => _audioChannelSO.OnAudioPlayRequested(_dashAudio, _audioConfig);
}
