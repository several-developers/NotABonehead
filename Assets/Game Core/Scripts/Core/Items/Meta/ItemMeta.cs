using GameCore.AllConstants;
using UbicaEditor;

namespace GameCore.Items
{
    public abstract class ItemMeta : EditorMeta
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override string GetMetaCategory() =>
            EditorConstants.ItemsCategory;
    }
}