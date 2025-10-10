using System;

namespace Features.MainMenu.App
{
    public interface IMainMenuDialogue
    {
        void Show(Action onExit, Action onNewGame);
    }
}