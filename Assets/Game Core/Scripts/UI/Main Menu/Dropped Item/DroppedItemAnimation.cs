using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.UI.MainMenu.DroppedItem
{
    [Serializable]
    public class DroppedItemAnimation
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private float _minScale;

        [SerializeField, Min(0)]
        private float _maxScale;

        [SerializeField, Min(0.1f)]
        private float _scaleDuration = 1f;

        [SerializeField]
        private Ease _scaleEase;

        [Title(Constants.References)]
        [SerializeField, Required]
        private RectTransform _targetRT;

        // FIELDS: --------------------------------------------------------------------------------

        private Tweener _scaleTN;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void StartAnimation()
        {
            StopAnimation();
            Scale(toMax: false);
        }

        public void StopAnimation()
        {
            Dispose();
            _scaleTN = _targetRT.DOScale(_maxScale, 0);
        }

        public void Dispose() =>
            _scaleTN.Kill();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Scale(bool toMax)
        {
            Dispose();
            
            float value = toMax ? _maxScale : _minScale;
            _scaleTN = _targetRT.DOScale(value, _scaleDuration)
                .SetEase(_scaleEase)
                .OnComplete(() => Scale(!toMax));
        }
    }
}