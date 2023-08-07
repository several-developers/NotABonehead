using GameCore.Infrastructure.Services.Global.Data;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameCore
{
    public class StageInfoView : MonoBehaviour
    {
        [Inject]
        private void Construct(IGameDataService gameDataService)
        {
            int level = gameDataService.GetCurrentLevel();
            _stageTMP.text = $"Stage {level}";
        }
        
        public TextMeshProUGUI _stageTMP;
    }
}
