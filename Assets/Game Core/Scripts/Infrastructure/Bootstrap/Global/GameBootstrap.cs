using UnityEngine;

namespace GameCore.Infrastructure.Bootstrap.Global
{
    public class GameBootstrap : MonoBehaviour
    {
        // FIELDS: --------------------------------------------------------------------------------

        private const int TargetFrameRate = 60;
        
        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            Application.targetFrameRate = TargetFrameRate;
    }
}