using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Other
{
    [Serializable]
    public class PlayerCharacterAnimation
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Vector3 _startScale;
        
        [SerializeField]
        private Vector3 _minScale;
        
        [SerializeField, Min(0)]
        private float _scaleDuration = 0.3f;

        [SerializeField]
        private Ease _scaleEase;

        [SerializeField, Required]
        private Transform _target;

        // FIELDS: --------------------------------------------------------------------------------

        private Tweener _scaleTN;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void StartAnimation() => Scale(scaleDown: true);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Scale(bool scaleDown)
        {
            Vector3 scale = scaleDown ? _minScale : _startScale;
            
            _scaleTN.Kill();
            _scaleTN = _target
                .DOScale(scale, _scaleDuration)
                .SetEase(_scaleEase)
                .OnComplete(() =>
                {
                    if (scaleDown)
                        Scale(scaleDown: false);
                });
        }
    }
}