using DG.Tweening;
using UnityEngine;

namespace GameCore.UI.MainMenu.CoinsFlyAnimation
{
    public class GoldItem : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        public RectTransform targetRT;
        
        // FIELDS: --------------------------------------------------------------------------------

        private Tweener _moveTN;
        private Tweener _scaleTN;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void StopMoveAnimation() =>
            _moveTN.Kill();

        public void StopScaleAnimation() =>
            _scaleTN.Kill();

        public void CompleteMoveAnimation() =>
            _moveTN.Complete();
        
        public void CompleteScaleAnimation() =>
            _scaleTN.Complete();

        public void MoveToPosition(Vector2 position, float duration, Ease ease)
        {
            StopMoveAnimation();
            
            _moveTN = targetRT
                .DOAnchorPos(position, duration)
                .SetEase(ease)
                .SetLink(gameObject);
        }

        public void Scale(float scale, float duration, Ease ease)
        {
            StopScaleAnimation();

            _scaleTN = targetRT
                .DOScale(scale, duration)
                .SetEase(ease)
                .SetLink(gameObject);
        }
    }
}