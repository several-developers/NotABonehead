using GameCore.Utilities;
using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Battle.Monsters
{
    public class AvailableMonstersListMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.References)]
        [SerializeField, Required]
        private MonsterMeta[] _monstersMeta;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public MonsterMeta GetMonsterMetaByIndex(int index)
        {
            index = Mathf.Clamp(value: index, min: 0, max: _monstersMeta.Length - 1);
            return _monstersMeta[index];
        }
        
        public int GetMonstersAmount() =>
            _monstersMeta.Length;

        public override string GetMetaCategory() =>
            EditorConstants.MonstersCategory;

#if UNITY_EDITOR
        // DEBUG BUTTONS: -------------------------------------------------------------------------

        [Title(Constants.DebugButtons)]
        [Button(buttonSize: 25), DisableInPlayMode]
        private void DebugFindAllMonstersMeta() =>
            _monstersMeta = GlobalUtilities.FindAllMeta<MonsterMeta>().ToArray();
#endif
    }
}