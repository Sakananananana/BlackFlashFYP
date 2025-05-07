using UnityEngine;
using UnityEngine.UI;
using PlayerInputSystem;
using System.Collections;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Slider _holdProgressBar;

    private Coroutine _holdProgressCoroutine;
    private float _holdDuration;
    private float _startTime;
    private float _timer;

    private void OnEnable()
    {
        _inputReader.WarpEvent += ResetHoldProgress;
        _inputReader.StartWarpEvent += StartHoldProgress;
        _inputReader.CancelWarpEvent += ResetHoldProgress;
    }

    private void OnDisable()
    {
        _inputReader.WarpEvent -= ResetHoldProgress;
        _inputReader.StartWarpEvent -= StartHoldProgress;
        _inputReader.CancelWarpEvent -= ResetHoldProgress;
    }

    private void StartHoldProgress()
    {
        _holdProgressBar.value = 0;

        _startTime = Time.time;
        _holdDuration = _startTime + 1.5f;
        _timer = 0f;

        if (_holdProgressCoroutine != null)
            StopCoroutine(_holdProgressCoroutine);

        _holdProgressCoroutine = StartCoroutine(HoldProgress());
    }

    private void ResetHoldProgress()
    {
        if (_holdProgressCoroutine != null)
        {
            StopCoroutine(_holdProgressCoroutine);
            _holdProgressCoroutine = null;
        }

        _timer = 0f;
        _holdProgressBar.value = 0;
    }

    private IEnumerator HoldProgress()
    {
        while (_holdDuration > Time.time)
        {
            _timer += Time.deltaTime;
            _holdProgressBar.value = Mathf.Clamp01(_timer / 1.5f);

            yield return null;
        }
    }
}
