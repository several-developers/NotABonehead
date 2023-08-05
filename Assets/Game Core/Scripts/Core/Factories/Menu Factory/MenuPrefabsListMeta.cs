using GameCore.UI.Global.MenuView;
using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Factories
{
    public class MenuPrefabsListMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required, AssetsOnly]
        [ListDrawerSettings(AlwaysAddDefaultValue = true)]
        private MenuView[] _menuPrefabs;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public MenuView[] GetMenuPrefabs() => _menuPrefabs;
    }
}