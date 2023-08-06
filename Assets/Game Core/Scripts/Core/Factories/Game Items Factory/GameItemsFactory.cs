using System;
using System.Collections.Generic;
using GameCore.Infrastructure.Providers.Global;
using GameCore.UI.MainMenu.GameItems;
using UnityEngine;
using Zenject;

namespace GameCore.Factories
{
    public class GameItemsFactory
    {
         // CONSTRUCTORS: --------------------------------------------------------------------------

        public GameItemsFactory(DiContainer diContainer, IAssetsProvider assetsProvider)
        {
            _diContainer = diContainer;
            _gameItemsDictionary = new Dictionary<Type, GameItemView>(capacity: 12);

            SetupGameItemsDictionary(assetsProvider);
        }

        // FIELDS: --------------------------------------------------------------------------------

        private static DiContainer _diContainer;
        private static Dictionary<Type, GameItemView> _gameItemsDictionary;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static GameItemView Create<TItem>(Transform container)
            where TItem : GameItemView
        {
            TItem item = GetGameItemView<TItem>();
            return _diContainer.InstantiatePrefabForComponent<GameItemView>(item, container);
        }
        
        public static GameItemView Create<TItem, TParams>(Transform container, TParams t)
            where TItem : GameItemView, IComplexGameItemView<TParams>
            where TParams : class
        {
            TItem item = GetGameItemView<TItem>();
            GameItemView gameItemInstance = _diContainer.InstantiatePrefabForComponent<GameItemView>(item, container);
            (gameItemInstance as TItem)?.Setup(t);
            return gameItemInstance;
        }

        public static GameItemView Create<TItem, TParams, TMenuParams>(Transform container, TParams t1, TMenuParams t2)
            where TItem : GameItemView, IComplexGameItemView<TParams, TMenuParams>
            where TParams : class
            where TMenuParams : struct
        {
            TItem item = GetGameItemView<TItem>();
            GameItemView gameItemInstance = _diContainer.InstantiatePrefabForComponent<GameItemView>(item, container);
            (gameItemInstance as TItem)?.Setup(t1, t2);
            return gameItemInstance;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static void SetupGameItemsDictionary(IAssetsProvider assetsProvider)
        {
            GameItemPrefabsListMeta gameItemPrefabsListMeta = assetsProvider.GetGameItemPrefabsListMeta();
            GameItemView[] gameItemReferences = gameItemPrefabsListMeta.GetGameItemReferences();

            foreach (GameItemView gameItemView in gameItemReferences)
            {
                if (CheckForNull(gameItemView))
                    continue;

                Type type = gameItemView.GetType();
                _gameItemsDictionary.Add(type, gameItemView);
            }

            bool CheckForNull(GameItemView gameItemPrefab)
            {
                if (gameItemPrefab != null)
                    return false;
                
                Debug.LogError("Missing <gb>game item prefab</gb> reference!");
                
                return true;
            }
        }

        private static TItem GetGameItemView<TItem>() where TItem : GameItemView, IGameItemView =>
            _gameItemsDictionary[typeof(TItem)] as TItem;
    }
}