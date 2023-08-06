using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.UI.Global.Other
{
    public class WindowPopupAnimation : MonoBehaviour
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Range(0, 1)]
        private float _scalePercent = 0.5f;
        
        [SerializeField, Min(0)]
        private float _scaleTime = 0.35f;
        
        [SerializeField]
        private Ease _scaleEase = Ease.OutBack;

        [Title(Constants.References)]
        [SerializeField, Required]
        private RectTransform _menuRT;

        // FIELDS: --------------------------------------------------------------------------------
        
        private Tweener _menuTN;
        private float _menuStartScale = 1f;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake()
        {
            _menuStartScale = _menuRT.localScale.x;
            _menuTN = _menuRT
                .DOScale(_menuStartScale * _scalePercent, 0)
                .SetUpdate(true);
        }

        private void Start() => PlayAnimation();

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void PlayAnimation()
        {
            _menuTN.Complete();
            _menuRT
                .DOScale(_menuStartScale * _scalePercent, 0)
                .SetUpdate(true);
            
            _menuTN.Complete();
            _menuTN = _menuRT
                .DOScale(_menuStartScale, _scaleTime)
                .SetEase(_scaleEase)
                .SetUpdate(true);
        }

        // DEBUG BUTTONS: -------------------------------------------------------------------------
        
        [Title(Constants.DebugButtons)]
        [Button(30), DisableInEditorMode, GUIColor(0.5f, 0.5f, 1)]
        private void DebugPlayAnimation() => PlayAnimation();
    }
}