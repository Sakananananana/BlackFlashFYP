using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class FadeController : MonoBehaviour
{
    [SerializeField] private Slider _fadeSlider;

    [SerializeField] private FadeEventChannelSO _fadeEvent;
    [SerializeField] private Image _image;

    [SerializeField] private VoidEventChannelSO _onRoomExit;
    [SerializeField] private VoidEventChannelSO _onRoomEnter;

    private void OnEnable()
    {
        _fadeEvent.OnEventRaised += InitializeFade;
    }

    private void OnDisable()
    {
        _fadeEvent.OnEventRaised -= InitializeFade;
    }

    private void InitializeFade(bool fadeIn, float duration)
    {
        Debug.Log(fadeIn);

        if (fadeIn == true)
            StartCoroutine(FadeIn(duration, 1));
        else if (fadeIn == false)
            StartCoroutine(FadeOut(duration, 0));
    }

    private IEnumerator FadeIn(float duration, int targetAlpha)
    {
        float startAlpha = _image.color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            Color c = _image.color;
            c.a = alpha;
            _image.color = c;

            yield return null;
        }

        Color finalColor = _image.color;
        finalColor.a = targetAlpha;
        _image.color = finalColor;
    }

    private IEnumerator FadeOut(float duration, int targetAlpha)
    {
        float startAlpha = _image.color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            Color c = _image.color;
            c.a = alpha;
            _image.color = c;

            yield return null;
        }

        Color finalColor = _image.color;
        finalColor.a = targetAlpha;
        _image.color = finalColor;
    }
}
