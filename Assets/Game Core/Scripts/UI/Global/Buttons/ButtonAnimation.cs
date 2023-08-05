using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameCore.UI.Global.Buttons
{
    public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Settings)]
        [SerializeField]
        private bool _checkInteractable;

        [SerializeField, Required]
        [ShowIf(nameof(_checkInteractable))]
        private Button _button;

        [SerializeField, Min(0)]
        private float _scaleTime = 0.15f;

        [SerializeField]
        private Vector2 _scale = new(0.9f, 0.9f);

        [Title(References)]
        [InfoBox("Missing 'Rect Transform'!", InfoMessageType.Error, "@_rectTransform == null")]
        [SerializeField]
        private RectTransform _rectTransform;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsEnabled { get; set; } = true;

        // FIELDS: --------------------------------------------------------------------------------

        private const string Settings = "Settings";
        private const string References = "References";

        private Tweener _scaleTN;
        private Vector3 _startScale;
        private Vector3 _finalScale;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() =>
            _startScale = _rectTransform.localScale;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnButtonDown()
        {
            if (!IsEnabled) return;

            _scaleTN.Complete();
            _finalScale = _startScale * _scale;
            _finalScale.z = _finalScale.x;

            _scaleTN = _rectTransform
                .DOScale(_finalScale, _scaleTime)
                .SetUpdate(true)
                .SetLink(gameObject);
        }

        private void OnButtonUp()
        {
            if (!IsEnabled) return;

            _scaleTN.Complete();

            _scaleTN = _rectTransform
                .DOScale(_startScale, _scaleTime)
                .SetUpdate(true)
                .SetLink(gameObject);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_checkInteractable && !_button.interactable)
                return;

            OnButtonDown();
        }

        public void OnPointerUp(PointerEventData eventData) => OnButtonUp();
    }
}