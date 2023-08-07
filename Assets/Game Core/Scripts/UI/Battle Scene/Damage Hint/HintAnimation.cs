using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace GameCore.UI.BattleScene.DamageHints
{
    public class HintAnimation : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [TitleGroup("Settings")]
        [BoxGroup("Settings/In", showLabel: false)]
        [SerializeField] private float _animationDuration;

        [TitleGroup("Scale Settings")]
        [BoxGroup("Scale Settings/In", showLabel: false)]
        [SerializeField] private Vector2 _punchScale;

        [BoxGroup("Scale Settings/In")]
        [SerializeField] private float _punchScaleTime;

        [Space(5)]

        [BoxGroup("Scale Settings/In")]
        [SerializeField] private float _scaleDownDelay;
        
        [BoxGroup("Scale Settings/In")]
        [SerializeField] private float _scaleDownTime;
        [BoxGroup("Scale Settings/In")]
        [SerializeField] private float _scaleDown;
        
        [BoxGroup("Scale Settings/In")]
        [SerializeField] private Ease _scaleDownEase;


        [TitleGroup("Fade Settings")]
        [BoxGroup("Fade Settings/In", showLabel: false)]
        [SerializeField] private float _fadeInTime;
        
        [BoxGroup("Fade Settings/In")]
        [SerializeField] private Ease _fadeInEase;

        [Space(5)]

        [BoxGroup("Fade Settings/In")]
        [SerializeField] private float _fadeOutDelay;
        
        [BoxGroup("Fade Settings/In")]
        [SerializeField] private float _fadeOutTime;
        
        [BoxGroup("Fade Settings/In")]
        [SerializeField] private Ease _fadeOutEase;


        [TitleGroup("Move Settings")]
        [BoxGroup("Move Settings/In", showLabel: false)]
        [SerializeField] private float _offset;

        [BoxGroup("Move Settings/In")]
        [SerializeField] private float _moveTime;

        [BoxGroup("Move Settings/In")]
        [SerializeField] private Ease _moveEase;


        [TitleGroup("References")]
        [BoxGroup("References/In", showLabel: false)]
        [SerializeField] private RectTransform _targetRT;
        
        [BoxGroup("References/In")]
        [SerializeField] private CanvasGroup _canvasGroup;

        [TitleGroup("Events")]
        [SerializeField] private UnityEvent _onAnimationEndedEvent;

        // FIELDS: --------------------------------------------------------------------------------
        
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

        private void OnAnimationEnded()
        {
            ResetAnimation();
            _onAnimationEndedEvent.Invoke();
        }
    }
}