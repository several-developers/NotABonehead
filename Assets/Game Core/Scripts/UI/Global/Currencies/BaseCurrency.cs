using System.Collections;
using GameCore.Events;
using GameCore.Infrastructure.Services.Global.Data;
using GameCore.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameCore.UI.Global.Currency
{
    public abstract class BaseCurrency : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IPlayerDataService playerDataService) =>
            PlayerDataService = playerDataService;

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(0)]
        private float _valueUpdateTime = 0.75f;

        [SerializeField]
        private bool _updateOnEnable;
        
        [Title(Constants.References)]
        [SerializeField, Required]
        private TextMeshProUGUI _valueTMP;
        
        // FIELDS: --------------------------------------------------------------------------------

        protected IPlayerDataService PlayerDataService;
        protected float LastCurrency;
        
        private Coroutine _valueUpdaterCO;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        protected virtual void Awake() =>
            GlobalEvents.OnCurrencyChanged += OnCurrencyChanged;

        private void Start() => UpdateValueInstant();

        protected virtual void OnDestroy() =>
            GlobalEvents.OnCurrencyChanged -= OnCurrencyChanged;

        private void OnEnable()
        {
            if (!_updateOnEnable)
                return;
            
            UpdateValueInstant();
        }

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected abstract void UpdateValue();

        protected abstract void UpdateValueInstant();

        protected void StartValueUpdater(float endValue)
        {
            if (!gameObject.activeSelf)
                return;
            
            _valueUpdaterCO = StartCoroutine(ValueUpdaterCO(endValue));
        }

        protected void StopValueUpdater()
        {
            if (_valueUpdaterCO == null)
                return;

            StopCoroutine(_valueUpdaterCO);
        }

        protected void UpdateValueText(int value) =>
            _valueTMP.text = GlobalUtilities.FormatNumber(value);

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private IEnumerator ValueUpdaterCO(float endValue)
        {
            float startValue = LastCurrency;
            float elapsedTime = 0;

            while (elapsedTime < _valueUpdateTime)
            {
                elapsedTime += Time.deltaTime;
                LastCurrency = Mathf.Lerp(startValue, endValue, elapsedTime / _valueUpdateTime);

                UpdateValueText((int)LastCurrency);

                yield return null;
            }

            LastCurrency = endValue;

            UpdateValueText((int)LastCurrency);
        }

        // EVENTS RECEIVERS: ----------------------------------------------------------------------
        
        private void OnCurrencyChanged() => UpdateValue();
    }
}
