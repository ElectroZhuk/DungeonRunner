using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image _filledImage;

    private static SceneTransition _instance;
    private static bool _shouldPlayOpening = false;
    private Animator _animator;
    private AsyncOperation _loadingSceneOperation;

    private void Start()
    {
        _instance = this;
        _animator = GetComponent<Animator>();

        if (_shouldPlayOpening)
            _animator.SetTrigger(AnimatorLoadingScreenController.Params.SceneEnter);
    }

    private void Update()
    {
        if (_loadingSceneOperation != null)
        {
            _filledImage.fillAmount = _loadingSceneOperation.progress;
        }
    }

    public static void SwitchToScene(string sceneName)
    {
        _instance._animator.SetTrigger(AnimatorLoadingScreenController.Params.SceneExit);
        _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        _instance._loadingSceneOperation.allowSceneActivation = false;
    }

    public static void SwitchToScene(int sceneIndex)
    {
        _instance._animator.SetTrigger(AnimatorLoadingScreenController.Params.SceneExit);
        _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneIndex);
        _instance._loadingSceneOperation.allowSceneActivation = false;
    }

    public void OnAnimationOver()
    {
        _shouldPlayOpening = true;
        _loadingSceneOperation.allowSceneActivation = true;
    }
}
