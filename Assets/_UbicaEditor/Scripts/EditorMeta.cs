using Sirenix.OdinInspector;
using UnityEngine;

namespace UbicaEditor
{
    public abstract class EditorMeta : ScriptableObject
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [TitleGroup(MetaSettings)]
        [BoxGroup(MetaSettingsIn, showLabel: false), SerializeField]
        private string _metaName;

        // FIELDS: --------------------------------------------------------------------------------
        
        private const string MetaSettings = "Meta Settings";
        private const string MetaSettingsIn = "Meta Settings/In";
        private const string NoCategory = "No category";

        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnEnable() =>
            _metaName = name;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public string GetMetaName() => _metaName;
        
        public void SetMetaName(string newName) =>
            _metaName = newName;
        
        public virtual string GetMetaCategory() => NoCategory;
    }
}