using System;
using System.Collections.Generic;

namespace GameCore.Items
{
    public class ItemViewParams
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public ItemViewParams(string itemKeyOrID, bool useItemKey, bool isInteractable)
        {
            ItemKeyOrID = itemKeyOrID;
            UseItemKey = useItemKey;
            IsInteractable = isInteractable;
            OnItemClickedEvent = null;

            _gameItemParams = new Dictionary<Type, IItemViewParam>(capacity: 4);
        }

        // PROPERTIES: ----------------------------------------------------------------------------

        public string ItemKeyOrID { get; }
        public bool UseItemKey { get; }
        public bool IsInteractable { get; }

        // FIELDS: --------------------------------------------------------------------------------
        
        public event Action<ItemViewParams> OnItemClickedEvent;

        private readonly Dictionary<Type, IItemViewParam> _gameItemParams;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void AddParam(IItemViewParam itemViewParam) =>
            _gameItemParams.TryAdd(itemViewParam.GetType(), itemViewParam);

        public T GetParam<T>() where T : class, IItemViewParam
        {
            if (_gameItemParams.TryGetValue(typeof(T), out IItemViewParam itemViewParam))
                return itemViewParam as T;

            T defaultItemViewParam = Activator.CreateInstance<T>();
            return defaultItemViewParam;
        }

        public void SendClickEvent() =>
            OnItemClickedEvent?.Invoke(this);
    }
}