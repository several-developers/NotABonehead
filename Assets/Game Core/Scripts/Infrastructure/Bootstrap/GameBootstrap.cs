using UnityEngine;

namespace GameCore.Infrastructure.Bootstrap
{
    // ----------------------------------------------------------------------
    //      - Находится в Resources/Project Context.prefab
    //      - Игра корректно работает при запуске из любой сцены.
    // ----------------------------------------------------------------------
    
    public class GameBootstrap : MonoBehaviour
    {
        // FIELDS: --------------------------------------------------------------------------------

        private const int TargetFrameRate = 60;
        
        // GAME ENGINE METHODS: -------------------------------------------------------------------

        private void Awake() =>
            Application.targetFrameRate = TargetFrameRate;
    }
}