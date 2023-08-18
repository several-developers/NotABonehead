using GameCore.Configs;
using GameCore.Enums;
using GameCore.Infrastructure.Providers.Global;
using GameCore.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameCore.UI.MainMenu.ItemsDropChancesMenu
{
    public class RarityChanceItemView : MonoBehaviour
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        [Inject]
        private void Construct(IConfigsProvider configsProvider) =>
            _configsProvider = configsProvider;

        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField]
        private ItemRarity _itemRarity;
        
        [Title(Constants.References)]
        [SerializeField, Required]
        private TextMeshProUGUI _chanceTMP;

        // FIELDS: --------------------------------------------------------------------------------

        private IConfigsProvider _configsProvider;

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Start() => UpdateValueChance();

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void UpdateValueChance()
        {
            ItemsDropChancesConfigMeta itemsDropChancesConfig = _configsProvider.GetItemsDropChancesConfig();
            float dropChance = itemsDropChancesConfig.GetItemDropChance(_itemRarity);
            
            SetValueChance(dropChance);
        }

        private void SetValueChance(float value)
        {
            string text = value.ToString("G");
            text = text.ReplaceCommaWithDot();

            if (text.Contains('.'))
            {
                int dotIndex = text.IndexOf('.') + 3;
                dotIndex = Mathf.Min(dotIndex, text.Length);
                text = text.Substring(0, dotIndex);
            }

            _chanceTMP.text = $"{text}%";
        }
    }
}
