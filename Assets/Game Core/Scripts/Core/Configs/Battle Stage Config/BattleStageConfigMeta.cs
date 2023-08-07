using Sirenix.OdinInspector;
using UbicaEditor;
using UnityEngine;

namespace GameCore.Configs
{
    public class BattleStageConfigMeta : EditorMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [Title(Constants.Settings)]
        [SerializeField, Min(1), SuffixLabel("seconds", overlay: true)]
        private float _attackDelay = 3f;

        [SerializeField, Min(0), SuffixLabel("%", overlay: true)]
        private float _statsIncreasePerLevel = 0.1f;

        // PROPERTIES: ----------------------------------------------------------------------------

        public float AttackDelay => _attackDelay;
        public float StatsIncreasePerLevel => _statsIncreasePerLevel;
    }
}