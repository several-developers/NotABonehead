using System;

namespace GameCore.MainMenu
{
    public interface IPlayerPreviewObserver
    {
        event Action OnClickedEvent;
        void SendClickEvent();
    }
}