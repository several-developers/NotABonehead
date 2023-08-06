using GameCore.UI.MainMenu.GameItems;
using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Factories
{
    public class GameItemPrefabsListMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(References)]
        [SerializeField, AssetsOnly]
        private GameItemView[] _gameItemReferences;

        // FIELDS: --------------------------------------------------------------------------------

        private const string References = "References";

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public GameItemView[] GetGameItemReferences() => _gameItemReferences;
    }
}