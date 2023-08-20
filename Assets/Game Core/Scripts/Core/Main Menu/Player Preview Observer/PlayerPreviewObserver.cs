using System;

namespace GameCore.MainMenu
{
    public class PlayerPreviewObserver : IPlayerPreviewObserver
    {
        // FIELDS: --------------------------------------------------------------------------------

        public event Action OnClickedEvent;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SendClickEvent() =>
            OnClickedEvent?.Invoke();
    }
}