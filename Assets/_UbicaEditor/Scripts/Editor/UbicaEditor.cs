#if UNITY_EDITOR
using System.IO;
using System.Linq;
using GameCore.Items;
using GameCore.Battle.Monsters;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace UbicaEditor
{
    // 
    // Be sure to check out OdinMenuStyleExample.cs as well. It shows you various ways to customize the look and behaviour of OdinMenuTrees.
    // 

    public class UbicaEditor : OdinMenuEditorWindow
    {
        // FIELDS: --------------------------------------------------------------------------------

        private const string UbicaEditorMenuItem = "🕹 Not A Bonehead/⚙ Ubica Editor";

        private EditorMenuType _editorMenuType;

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(true);
            SetupMenuStyle(tree);

            tree.AddAllAssetsAtPath(menuPath: "",
                assetFolderPath: "Assets/Resources/Game Data/",
                typeof(EditorMeta),
                includeSubDirectories: true);
            
            // Add icons to items
            tree.EnumerateTree().AddIcons<ItemMeta>(m => m.Icon);
            tree.EnumerateTree().AddIcons<MonsterMeta>(m => m.Icon);

            // Add drag handles to items, so they can be easily dragged...
            tree.EnumerateTree().Where(x => x.Value as ItemMeta).ForEach(AddDragHandles);
            tree.EnumerateTree().Where(x => x.Value as MonsterMeta).ForEach(AddDragHandles);

            tree.EnumerateTree().SortMenuItemsByName();
            tree.EnumerateTree().AddThumbnailIcons();

            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            if (MenuTree == null ||
                MenuTree.Selection == null)
            {
                return;
            }

            OdinMenuTreeSelection selection = MenuTree.Selection;
            OdinMenuItem selected = MenuTree.Selection.FirstOrDefault();
            int toolbarHeight = MenuTree.Config.SearchToolbarHeight;
            bool selectedIsNull = selected == null;
            EditorMeta selectedMeta = selection.SelectedValue as EditorMeta;
            string metaPath = "Assets/Resources/Game Data/";

            // Draws a toolbar with the name of the currently selected menu item.
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);

            // Draw label
            GUILayout.Label(selectedIsNull ? " " : selected.Name);

            // Create and Selects the newly created item in the editor
            if (SirenixEditorGUI.ToolbarButton("Create"))
            {
                if (selectedMeta != null)
                {
                    metaPath = AssetDatabase.GetAssetPath(selectedMeta);
                    string fileName = Path.GetFileName(metaPath);
                    metaPath = metaPath.Replace(fileName, "");
                }

                ScriptableObjectCreator.ShowDialog<EditorMeta>(metaPath, TrySelectMenuItemWithObject);
            }

            if (!selectedIsNull && selectedMeta)
            {
                if (SirenixEditorGUI.ToolbarButton("Duplicate"))
                {
                    metaPath = AssetDatabase.GetAssetPath(selectedMeta);
                    ScriptableObjectCreator.Duplicate(selectedMeta, metaPath, ScriptableObjectUtility.SaveAsset);
                }
                
                if (SirenixEditorGUI.ToolbarButton("Save"))
                {
                    metaPath = AssetDatabase.GetAssetPath(selectedMeta);
                    string newName = selectedMeta.GetNewName();
                    
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        AssetDatabase.RenameAsset(metaPath, newName);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();

                        TrySelectMenuItemWithObject(selectedMeta);
                    }
                    
                }
                
                if (SirenixEditorGUI.ToolbarButton("Delete"))
                {
                    metaPath = AssetDatabase.GetAssetPath(selectedMeta);

                    AssetDatabase.DeleteAsset(metaPath);
                    AssetDatabase.SaveAssets();
                }
            }

            SirenixEditorGUI.EndHorizontalToolbar();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        [MenuItem(UbicaEditorMenuItem)]
        private static void OpenWindow()
        {
            var window = GetWindow<UbicaEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        private void SetupMenuStyle(OdinMenuTree tree)
        {
            tree.Config.DrawSearchToolbar = true;
            tree.DefaultMenuStyle = OdinMenuStyle.TreeViewStyle;
            tree.DefaultMenuStyle.Height = 28;
            tree.DefaultMenuStyle.IndentAmount = 12;
            tree.DefaultMenuStyle.IconSize = 26;
            tree.DefaultMenuStyle.NotSelectedIconAlpha = 1;
            tree.DefaultMenuStyle.IconPadding = 4;
            tree.DefaultMenuStyle.SelectedColorDarkSkin = EditorDatabase.SelectedColor;
            tree.DefaultMenuStyle.SelectedInactiveColorDarkSkin = EditorDatabase.SelectedInactiveColor;

            //tree.Add("Menu Style", tree.DefaultMenuStyle);
        }

        private void AddDragHandles(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += x =>
                DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, false, false);
        }
    }
}
#endif