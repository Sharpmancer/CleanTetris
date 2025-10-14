using System;

namespace Features.MainMenu.App
{
    public interface IMainMenuDialogue
    {
        // TODO: create dialogue and button abstractions, create a UI lib
        void Show(Action onExit, Action onNewGame, Action onContinue);
        void SetContinueButtonInteractable(bool value);
    }
}