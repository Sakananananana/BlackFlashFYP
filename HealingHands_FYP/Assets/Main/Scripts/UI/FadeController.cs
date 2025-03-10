using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Slider _fadeSlider;

    [SerializeField] private VoidEventChannelSO _onRoomExit;
    [SerializeField] private VoidEventChannelSO _onRoomEnter;

    private void OnEnable()
    {
        _onRoomExit.OnEventRaised += FadeInOut;
    }

    private void OnDisable()
    {
        _onRoomExit.OnEventRaised -= FadeInOut;
    }

    private void FadeInOut()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (_fadeSlider.value < 1f)
        {
            _fadeSlider.value += 0.01f;
            yield return null;
        }

        StartCoroutine(FadeOut());
        _onRoomEnter.RaiseEvent();
    }

    private IEnumerator FadeOut()
    {
        while (_fadeSlider.value > 0)
        {
            _fadeSlider.value -= 0.01f;
            yield return null;
        }
    }
}
