using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.UI.BattleScene.DamageHints
{
    public class HintAnimation : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [TitleGroup(Constants.Settings)]
        [BoxGroup(Constants.SettingsIn, showLabel: false), SerializeField]
        private float _animationDuration;

        
        [TitleGroup(ScaleSettings)]
        [BoxGroup(ScaleSettingsIn, showLabel: false), SerializeField]
        private Vector2 _punchScale;

        [BoxGroup(ScaleSettingsIn), SerializeField]
        private float _punchScaleTime;
        
        [BoxGroup(ScaleSettingsIn), SerializeField, Space(5)]
        private float _scaleDownDelay;

        [BoxGroup(ScaleSettingsIn), SerializeField]
        private float _scaleDownTime;

        [BoxGroup(ScaleSettingsIn), SerializeField]
        private float _scaleDown;

        [BoxGroup(ScaleSettingsIn), SerializeField]
        private Ease _scaleDownEase;


        [TitleGroup(FadeSettings)]
        [BoxGroup(FadeSettingsIn, showLabel: false), SerializeField]
        private float _fadeInTime;

        [BoxGroup(FadeSettingsIn), SerializeField]
        private Ease _fadeInEase;

        [BoxGroup(FadeSettingsIn), SerializeField, Space(5)]
        private float _fadeOutDelay;

        [BoxGroup(FadeSettingsIn), SerializeField]
        private float _fadeOutTime;

        [BoxGroup(FadeSettingsIn), SerializeField]
        private Ease _fadeOutEase;


        [TitleGroup(MoveSettings)]
        [BoxGroup(MoveSettingsIn, showLabel: false), SerializeField]
        private float _offset;

        [BoxGroup(MoveSettingsIn), SerializeField]
        private float _moveTime;

        [BoxGroup(MoveSettingsIn), SerializeField]
        private Ease _moveEase;


        [TitleGroup(References)]
        [BoxGroup(ReferencesIn, showLabel: false), SerializeField]
        private RectTransform _targetRT;

        [BoxGroup(ReferencesIn), SerializeField]
        private CanvasGroup _canvasGroup;

        // FIELDS: --------------------------------------------------------------------------------

        private const string ScaleSettings = "Scale Settings";
        private const string ScaleSettingsIn = "Scale Settings/In";
        private const string FadeSettings = "Fade Settings";
        private const string FadeSettingsIn = "Fade Settings/In";
        private const string MoveSettings = "Move Settings";
        private const string MoveSettingsIn = "Move Settings/In";
        private const string References = "References";
        private const string ReferencesIn = "References/In";
        
        private Tweener _fadeTN;
        private Tweener _scaleTN;
        private Tweener _moveTN;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() => ResetAnimation();

        // PUBLIC METHODS: ------------------------------------------------------------------------

        [Button]
        public void StartAnimation()
        {
            ScaleUpAnimation();
            MoveAnimation();
            FadeIn();

            Invoke(nameof(ScaleDownAnimation), _scaleDownDelay);
            Invoke(nameof(FadeOut), _fadeOutDelay);
            Invoke(nameof(OnAnimationEnded), _animationDuration);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        [Button]
        private void ResetAnimation()
        {
            CancelInvoke(nameof(ScaleDownAnimation));
            CancelInvoke(nameof(FadeOut));

            _scaleTN.Complete();
            _moveTN.Complete();
            _fadeTN.Complete();

            _targetRT.DOScale(0, 0);
            _canvasGroup.DOFade(0, 0);
            _targetRT.DOAnchorPosY(0, 0);
        }

        private void ScaleUpAnimation() =>
            _scaleTN = _targetRT.DOPunchScale(_punchScale, _punchScaleTime, 1);

        private void ScaleDownAnimation()
        {
            _scaleTN.Kill();
            _scaleTN = _targetRT.DOScale(_scaleDown, _scaleDownTime).SetEase(_scaleDownEase);
        }

        private void FadeIn() =>
            _fadeTN = _canvasGroup.DOFade(1, _fadeInTime).SetEase(_fadeInEase);

        private void FadeOut()
        {
            _fadeTN.Kill();
            _fadeTN = _canvasGroup.DOFade(0, _fadeOutTime).SetEase(_fadeOutEase);
        }

        private void MoveAnimation() =>
            _moveTN = _targetRT.DOAnchorPosY(_offset, _moveTime).SetEase(_moveEase);

        private void OnAnimationEnded() => ResetAnimation();
    }
}