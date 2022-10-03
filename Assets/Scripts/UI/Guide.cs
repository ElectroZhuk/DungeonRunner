using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] private bool _needGuide;
    [SerializeField] private Slide[] _slides;

    private int _currentSlide = 0;

    private void Awake()
    {
        gameObject.SetActive(_needGuide);
        _slides[_currentSlide].Switching += NextSlide;
    }

    private void NextSlide()
    {
        Slide previousSlide = _slides[_currentSlide];
        previousSlide.gameObject.SetActive(false);
        previousSlide.Switching -= NextSlide;

        if (_currentSlide + 1 < _slides.Length)
        {
            _currentSlide++;
            Slide currentSlide = _slides[_currentSlide];
            currentSlide.gameObject.SetActive(true);
            currentSlide.Switching += NextSlide;
        }
    }
}
