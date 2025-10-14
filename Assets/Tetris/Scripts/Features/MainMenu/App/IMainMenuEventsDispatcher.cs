using System;

namespace Features.MainMenu.App
{
    public interface IMainMenuEventsDispatcher
    {
        event Action OnNewGame;
    }
}