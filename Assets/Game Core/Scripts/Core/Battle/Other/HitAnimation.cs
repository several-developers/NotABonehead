using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Battle
{
    public class HitAnimation : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private float _minScale = 0.6f;

        [SerializeField, Min(0)]
        private float _maxScale = 1f;

        [SerializeField, Min(0)]
        private float _scaleDuration = 0.15f;
        
        [SerializeField]
        private Ease _scaleEase = Ease.InOutQuad;

        [Title(Constants.References)]
        [SerializeField, Required]
        private Transform _targetRT;

        // FIELDS: --------------------------------------------------------------------------------

        private Tweener _scaleTN;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        [Button]
        protected void StartAnimation()
        {
            StopAnimation();
            ScaleDown();
        }
        
        [Button]
        protected void StopAnimation()
        {
            _scaleTN.Kill();

            _scaleTN = _targetRT
                .DOScale(_maxScale, 0)
                .SetLink(gameObject);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void ScaleDown()
        {
            _scaleTN = _targetRT
                .DOScale(_minScale, _scaleDuration)
                .SetEase(_scaleEase)
                .SetLink(gameObject)
                .OnComplete(ScaleUp);
        }

        private void ScaleUp()
        {
            _scaleTN = _targetRT
                .DOScale(_maxScale, _scaleDuration)
                .SetEase(_scaleEase)
                .SetLink(gameObject);
        }
    }
}
