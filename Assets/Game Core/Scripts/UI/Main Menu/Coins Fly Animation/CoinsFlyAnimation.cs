using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameCore.Enums;
using GameCore.Events;
using GameCore.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.UI.MainMenu.CoinsFlyAnimation
{
    public class CoinsFlyAnimation : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private float _wholeAnimationDelay;
        
        [SerializeField, Min(0)]
        private float _disableCanvasDelay = 3;
        
        [SerializeField, Min(0), Space(5)]
        private float _minDistance;
        
        [SerializeField, Min(0)]
        private float _maxDistance;

        [SerializeField, Min(0)]
        private float _minDropTime;
        
        [SerializeField, Min(0)]
        private float _maxDropTime;

        [SerializeField]
        private Ease _dropEase;

        [SerializeField, Min(0), Space(5)]
        private float _minFlyDelay;
        
        [SerializeField, Min(0)]
        private float _maxFlyDelay;
        
        [SerializeField, Min(0)]
        private float _minFlyTime;
        
        [SerializeField, Min(0)]
        private float _maxFlyTime;

        [SerializeField]
        private Ease _flyEase;

        [SerializeField, Min(0), Space(5)]
        private float _minScale;
        
        [SerializeField, Min(0)]
        private float _scaleTime;

        [SerializeField]
        private Ease _scaleEase;

        [Title(Constants.References)]
        [SerializeField, Required]
        private Canvas _canvas;
        
        [SerializeField, Required]
        private RectTransform _coinsTarget;

        [SerializeField, Required, Space(5)]
        private GoldItem[] _goldItems;
        
        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            GlobalEvents.OnCurrencyChanged += OnCurrencyChanged;

        private void OnDestroy() =>
            GlobalEvents.OnCurrencyChanged -= OnCurrencyChanged;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        [Button]
        public async void StartAnimation()
        {
            StopAnimation();
            DropCoins();
            _canvas.enabled = true;

            int delay = _disableCanvasDelay.ConvertToMilliseconds();
            bool isCanceled = await UniTask
                .Delay(delay, cancellationToken: this.GetCancellationTokenOnDestroy())
                .SuppressCancellationThrow();

            if (isCanceled)
                return;

            _canvas.enabled = false;
        }

        [Button]
        public void StopAnimation()
        {
            foreach (GoldItem goldItem in _goldItems)
            {
                goldItem.transform.SetParent(transform);
                goldItem.StopMoveAnimation();
                goldItem.MoveToPosition(Vector2.zero, 0, Ease.Linear);
                goldItem.StopScaleAnimation();
                goldItem.Scale(1, 0, Ease.Linear);
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async void StartAnimationWithDelay()
        {
            int delay = _wholeAnimationDelay.ConvertToMilliseconds();
            bool isCanceled = await UniTask
                .Delay(delay, cancellationToken: this.GetCancellationTokenOnDestroy())
                .SuppressCancellationThrow();

            if (isCanceled)
                return;
            
            StartAnimation();
        }
        
        private void DropCoins()
        {
            foreach (GoldItem goldItem in _goldItems)
            {
                float distance = Random.Range(_minDistance, _maxDistance);
                Vector2 movePosition = Random.insideUnitCircle * distance;
                float duration = Random.Range(_minDropTime, _maxDropTime);

                goldItem.CompleteMoveAnimation();
                goldItem.MoveToPosition(movePosition, duration, _dropEase);

                ScaleDownCoin(goldItem);
                ScaleUpCoin(goldItem);

                FlyToTarget(goldItem);
            }
        }

        private async void FlyToTarget(GoldItem goldItem)
        {
            float randomDelay = Random.Range(_minFlyDelay, _maxFlyDelay);
            int delay = randomDelay.ConvertToMilliseconds();

            bool isCanceled = await UniTask
                .Delay(delay, cancellationToken: this.GetCancellationTokenOnDestroy())
                .SuppressCancellationThrow();

            if (isCanceled)
                return;

            float flyTime = Random.Range(_minFlyTime, _maxFlyTime);
            goldItem.transform.SetParent(_coinsTarget);
            goldItem.MoveToPosition(Vector2.zero, flyTime, _flyEase);
        }

        private void ScaleDownCoin(GoldItem goldItem)
        {
            goldItem.CompleteScaleAnimation();
            goldItem.Scale(_minScale, 0, Ease.Linear);
        }

        private void ScaleUpCoin(GoldItem goldItem)
        {
            goldItem.CompleteScaleAnimation();
            goldItem.Scale(1, _scaleTime, _scaleEase);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        private void OnCurrencyChanged(CurrencyType currencyType)
        {
            if (currencyType != CurrencyType.Gold)
                return;
            
            StartAnimationWithDelay();
        }
    }
}
