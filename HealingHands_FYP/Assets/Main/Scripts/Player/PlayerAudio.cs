using System;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioData _dashAudio;
    [SerializeField] AudioData _slashAudio;
    [SerializeField] AudioData _footstepAudio;
    [SerializeField] AudioData _inventoryAudio;

    [SerializeField] AudioConfiguration _audioConfig;
    
    public void PlayFootstepAudio() => AudioManager.Instance.PlayRaisedAudio(_footstepAudio, _audioConfig);
    public void PlayInventoryAudio() => AudioManager.Instance.PlayRaisedAudio(_inventoryAudio, _audioConfig);
    public void PlaySlashAudio() => AudioManager.Instance.PlayRaisedAudio(_slashAudio, _audioConfig);
    public void PlayDashAudio() => AudioManager.Instance.PlayRaisedAudio(_dashAudio, _audioConfig);
}
