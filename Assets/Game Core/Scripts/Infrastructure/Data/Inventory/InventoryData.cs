using System;
using System.Collections.Generic;
using GameCore.AllConstants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Infrastructure.Data
{
    [Serializable]
    public class InventoryData : DataBase
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title("Items Data")]
        [SerializeField, Space(-10)]
        [ListDrawerSettings(ListElementLabelName = "Label")]
        private List<ItemData> _itemsData;

        [Title("Equipped Items Data")]
        [SerializeField]
        [ListDrawerSettings(ListElementLabelName = "Label")]
        private List<EquippedItemData> _equippedItemsData;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override string DataKey => Constants.InventoryDataKey;
    }
}