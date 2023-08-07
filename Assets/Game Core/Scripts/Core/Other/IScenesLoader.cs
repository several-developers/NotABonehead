using System;
using GameCore.Enums;

namespace GameCore.Other
{
    public interface IScenesLoader
    {
        event Action OnLoadingStarted;
        event Action OnLoadingFinished;
        void LoadScene(SceneName sceneName);
    }
}