using System;
using System.Collections;
using DG.Tweening;
using GameCore.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore.Other
{
    public class ScenesLoader : MonoBehaviour, IScenesLoader
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Settings)]
        [SerializeField, Min(0)]
        private float _fadeTime;

        [Title(References)]
        [SerializeField, Required]
        private Canvas _canvas;

        [SerializeField, Required]
        private CanvasGroup _canvasGroup;

        // FIELDS: --------------------------------------------------------------------------------

        public event Action OnLoadingStarted;
        public event Action OnLoadingFinished;

        private const string Settings = "Settings";
        private const string References = "References";
        private const string DebugButtons = "Debug Buttons";

        private bool _isLoading;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void LoadScene(SceneName sceneName)
        {
            if (_isLoading)
                return;

            FadeAnimation(true, sceneName);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void FadeAnimation(bool fadeIn, SceneName sceneName = SceneName.MainMenu)
        {
            if (fadeIn)
                _canvas.enabled = true;

            _canvasGroup
                .DOFade(fadeIn ? 1 : 0, _fadeTime)
                .OnComplete(() =>
                {
                    if (fadeIn)
                        StartCoroutine(SceneLoader(sceneName));
                    else
                        _canvas.enabled = false;
                });
        }

        private IEnumerator SceneLoader(SceneName sceneName)
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.

            OnLoadingStarted?.Invoke();
            _isLoading = true;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName.ToString());

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            OnLoadingFinished?.Invoke();
            _isLoading = false;
            FadeAnimation(false);
        }

        // DEBUG BUTTONS: -------------------------------------------------------------------------

        [Title(DebugButtons)]

        [Button(25)]
        private void DebugLoadMainMenu() =>
            LoadScene(SceneName.MainMenu);

        [Button(25)]
        private void DebugLoadBattle() =>
            LoadScene(SceneName.Battle);
    }
}