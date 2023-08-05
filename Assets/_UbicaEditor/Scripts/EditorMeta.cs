using Sirenix.OdinInspector;
using UnityEngine;

namespace UbicaEditor
{
    public abstract class EditorMeta : ScriptableObject
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [BoxGroup("T", showLabel: false), SerializeField]
        private string _metaName;
        
        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void OnEnable() =>
            _metaName = name;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public string GetNewName() => _metaName;
        
        public void SetNewName(string newName) =>
            _metaName = newName;
        
        public virtual string GetMetaCategory() => "No category";
    }
}