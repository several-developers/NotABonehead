#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace UbicaEditor
{
    public abstract class MenuDrawer
    {
        // CONSTRUCTORS: --------------------------------------------------------------------------

        protected MenuDrawer(UbicaEditor ubicaEditor) =>
            _ubicaEditor = ubicaEditor;

        // FIELDS: --------------------------------------------------------------------------------

        private readonly UbicaEditor _ubicaEditor;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public abstract void CreateMenu(OdinMenuTree tree);

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected Texture GetTexture(string texturePath) =>
            (Texture)EditorGUIUtility.Load(texturePath);
    }
}
#endif