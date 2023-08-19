using System;
using System.Collections.Generic;
using GameCore.Infrastructure.Providers.Global;
using GameCore.UI.Global.MenuView;
using GameCore.UI.Global.Other;
using UnityEngine;
using Zenject;

namespace GameCore.Factories
{
    public class MenuFactory
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public MenuFactory(DiContainer diContainer, IAssetsProvider assetsProvider)
        {
            _diContainer = diContainer;
            _menusDictionary = new Dictionary<Type, MenuView>(capacity: 64);

            SetupMenuDictionary(assetsProvider);
        }

        // FIELDS: --------------------------------------------------------------------------------

        private static DiContainer _diContainer;
        private static Dictionary<Type, MenuView> _menusDictionary;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void Create(Type menuType)
        {
            if (!_menusDictionary.ContainsKey(menuType))
                return;
            
            MenuView menuView = _menusDictionary[menuType];
            _diContainer.InstantiatePrefab(menuView, MainCanvas.Transform);
        }
        
        public static void Create<TPayload>(Type menuType, TPayload param)
        {
            if (!_menusDictionary.ContainsKey(menuType))
                return;
            
            MenuView menuView = _menusDictionary[menuType];
            GameObject menuInstance = _diContainer.InstantiatePrefab(menuView, MainCanvas.Transform);

            if (menuInstance.TryGetComponent(out IComplexMenuView<TPayload> component))
                component.Setup(param);
        }
        
        public static void Create<TMenu>() where TMenu : MenuView =>
            Create<TMenu>(MainCanvas.Transform);

        public static void Create<TMenu>(Transform container) where TMenu : MenuView
        {
            TMenu menu = GetMenu<TMenu>();
            _diContainer.InstantiatePrefabForComponent<TMenu>(menu, container);
        }

        public static void Create<TMenu, TPayload>(TPayload param)
            where TMenu : MenuView, IComplexMenuView<TPayload>
        {
            Create<TMenu, TPayload>(MainCanvas.Transform, param);
        }

        public static void Create<TMenu, TPayload>(Transform container, TPayload param)
            where TMenu : MenuView, IComplexMenuView<TPayload>
        {
            TMenu menu = GetMenu<TMenu>();
            TMenu menuInstance = _diContainer.InstantiatePrefabForComponent<TMenu>(menu, container);
            menuInstance.Setup(param);
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static void SetupMenuDictionary(IAssetsProvider assetsProvider)
        {
            MenuPrefabsListMeta menuPrefabsListMeta = assetsProvider.GetMenuPrefabsList();
            MenuView[] menuPrefabs = menuPrefabsListMeta.GetMenuPrefabs();

            foreach (MenuView menuPrefab in menuPrefabs)
            {
                if (CheckForNull(menuPrefab))
                    continue;

                Type type = menuPrefab.GetType();
                _menusDictionary.Add(type, menuPrefab);
            }

            bool CheckForNull(MenuView menuPrefab)
            {
                if (menuPrefab != null)
                    return false;
                
                Debug.LogError("Missing <gb>menu prefab</gb> reference!");

                return true;
            }
        }

        private static TMenu GetMenu<TMenu>() where TMenu : MenuView, IMenuView =>
            _menusDictionary[typeof(TMenu)] as TMenu;
    }
}