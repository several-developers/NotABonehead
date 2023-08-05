using GameCore.Enums;
using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Configs
{
    public class ItemsRarityConfigMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField]
        private ItemRarityConfig[] _itemRarityConfigs;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public ItemRarityConfig GetItemRarityConfig(ItemRarity itemRarity)
        {
            foreach (ItemRarityConfig itemRarityConfig in _itemRarityConfigs)
            {
                if (itemRarityConfig.ItemRarity == itemRarity)
                    return itemRarityConfig;
            }

            return _itemRarityConfigs[0];
        }
    }
}